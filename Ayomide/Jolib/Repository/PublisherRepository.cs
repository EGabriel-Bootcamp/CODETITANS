using Jolib.Data;
using Jolib.Entities;
using Jolib.Models;
using Microsoft.EntityFrameworkCore;

namespace Jolib.Repository
{
    public class PublisherRepository
    {

        private readonly DataContext _context;
        public PublisherRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> GetAllPublisher()
        {
            var publishers = await _context.Publisher.ToListAsync();
            if (publishers == null)
            {
                return new ApiResponse() { Code = "25", Description = "No publisher record found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = publishers };
        }

        public async Task<ApiResponse> GetPublisher(int id)
        {
            var publisher = await _context.Publisher.FirstOrDefaultAsync(x => x.ID == id);
            if (publisher == null)
            {
                return new ApiResponse() { Code = "25", Description = "publisher does not exists", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = publisher };


        }
        public async Task<ApiResponse> CreatePublisher(Publisher publisher)
        {
            var userExists =  _context.Publisher.Where((p) => (p.FirstName + p.LastName).ToLower() == (publisher.FirstName + publisher.LastName).ToLower()).Any();

            if (userExists)
                return new ApiResponse() { Code = "25", Description = "Publisher Exists", Data = null };

            await _context.Publisher.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return new ApiResponse() { Code = "00", Description = "Success", Data = publisher };


        }
        public async Task<ApiResponse> GetAuthorsAttachedToAPublisher(int id)
        {
            var publisher = await _context.Publisher.FirstOrDefaultAsync(x => x.ID == id);

            if (publisher == null)
                return new ApiResponse() { Code = "25", Description = "Publisher Does Not Exists", Data = null };

            
            return new ApiResponse() { Code = "00", Description = "Success", Data = publisher.Authors };


        }
    }
    }
