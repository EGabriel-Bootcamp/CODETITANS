using Jolib.Data;
using Jolib.Dtos;
using Jolib.Entities;
using Jolib.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Jolib.Repository
{
    public class AuthorRepository
    {

        private readonly DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> GetAllAuthors()
        {
            var authors = await _context.Author.ToListAsync();
            if (authors == null)
            {
                return new ApiResponse() { Code = "25", Description = "No authors record found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = authors };
        }

        public async Task<ApiResponse> GetAuthor(int id)
        {
            var author = await _context.Author.FirstOrDefaultAsync(x => x.ID == id);
            if (author == null)
            {
                return new ApiResponse() { Code = "25", Description = "author record not found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = author };

        }
        public async Task<ApiResponse> GetBooksAttachedToAuthor(int id)
        {
            var author = await _context.Author.FirstOrDefaultAsync(x => x.ID == id);

            if (author == null)
                return new ApiResponse() { Code = "25", Description = "Author Does Not Exists", Data = null };


            return new ApiResponse() { Code = "00", Description = "Success", Data = author.Books };


        }
        public async Task<ApiResponse> CreateAuthor(AuthorDto author)
        {

            var userExists = _context.Author.Where((p) => (p.FirstName + p.LastName).ToLower() == (author.FirstName + author.LastName).ToLower()).Any();

            if (userExists)
                return new ApiResponse() { Code = "25", Description = "Author Exists", Data = null };
            var publisher =await  _context.Publisher.FirstOrDefaultAsync(x => x.ID == author.PublisherId);
            if(publisher == null)
                return new ApiResponse() { Code = "25", Description = "Publisher does not Exists", Data = null };

            var newAuthor = new Author()
            {

                Publisher = publisher,

                FirstName = author.FirstName,
                LastName = author.LastName,

            };

            await _context.Author.AddAsync(newAuthor);
            await _context.SaveChangesAsync();
            return new ApiResponse() { Code = "00", Description = "Success", Data = author };


        }

    }
}
