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
        private List<Publisher> GetPublishers()
        {
            return _context.Publisher.Include((b) => b.Authors).ToList();

        }
        public ApiResponse GetAllPublisher()
        {
            var publishers = GetPublishers();
            if (publishers == null)
            {
                return new ApiResponse() { Code = "25", Description = "No publisher record found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = publishers };
        }

        public ApiResponse GetPublisher(int id)
        {
            var publisher = GetPublishers().FirstOrDefault(x => x.ID == id);
            if (publisher == null)
            {
                return new ApiResponse() { Code = "25", Description = "publisher does not exists", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = publisher };


        }
        public async Task<ApiResponse> CreatePublisher(Publisher publisher)
        {
            var userExists = GetPublishers().Where((p) => (p.FirstName + p.LastName).ToLower() == (publisher.FirstName + publisher.LastName).ToLower()).Any();

            if (userExists)
                return new ApiResponse() { Code = "25", Description = "Publisher Exists", Data = null };

            await _context.Publisher.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return new ApiResponse() { Code = "00", Description = "Success", Data = publisher };


        }
        public ApiResponse GetAuthorsAttachedToAPublisher(int id)
        {
            var publisher = GetPublishers().FirstOrDefault(x => x.ID == id);

            if (publisher == null)
                return new ApiResponse() { Code = "25", Description = "Publisher Does Not Exists", Data = null };

            
            return new ApiResponse() { Code = "00", Description = "Success", Data = publisher.Authors };


        }
    }
    }
