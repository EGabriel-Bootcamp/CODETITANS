using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UserMgt.Application.Contracts.Persistence;
using UserMgt.Application.Features.Models;
using UserMgt.Domain;

namespace UserMgt.Application.Features.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, APIResponse>
    {
        private readonly IUserRepository _userRepository;
        public IMapper _mapper;
        public APIResponse response;
        //private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public GetAllUsersQueryHandler(IUserRepository userRepository,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            //this._memoryCache = memoryCache;
            this._distributedCache = distributedCache;
            response = new();
        }

        public async Task<APIResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "usersList";
            string serializedUsersList;
            IEnumerable<User> users;
            
            //cachingdata
            var redisUsers = await _distributedCache.GetAsync(cacheKey);
            if (redisUsers != null)
            {
                serializedUsersList = Encoding.UTF8.GetString(redisUsers);
                users = JsonConvert.DeserializeObject<IEnumerable<User>>(serializedUsersList);
            }
            else
            {
                users = await _userRepository.GetAllAsync();
                
                serializedUsersList = JsonConvert.SerializeObject(users);
                redisUsers = Encoding.UTF8.GetBytes(serializedUsersList);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _distributedCache.SetAsync(cacheKey, redisUsers, options);
            }



            //Filter based on search
            if (request.SearchText != null)
            {
                users = users.Where(x => x.FirstName.ToLower().Contains(request.SearchText.ToLower())
                    || x.LastName.ToLower().Contains(request.SearchText.ToLower())
                    || x.Address.ToLower().Contains(request.SearchText.ToLower())
                    || x.City.ToLower().Contains(request.SearchText.ToLower())
                    || x.MaritalStatus.ToLower().Contains(request.SearchText.ToLower())
                    || (DateTime.Now.Year - x.DateOfBirth.Year).ToString() == request.SearchText);
            }

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = usersDto;

            return response;
        }
    }
}
