using Jolib.Data;
using Jolib.Dtos;
using Jolib.Entities;
using Jolib.Models;
using Microsoft.EntityFrameworkCore;

namespace Jolib.Repository
{
    public class BookRepository
    {

        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> GetAllBooks()
        {
            var books = await _context.Book.ToListAsync();
            if (books == null)
            {
                return new ApiResponse() { Code = "25", Description = "No Books record found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = books };
        }

        public async Task<ApiResponse> GetBook(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.ID == id);
            if (book == null)
            {
                return new ApiResponse() { Code = "25", Description = "book record not found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = book };

        }
        public async Task<ApiResponse> GetAuthorsAttachedToBook(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.ID == id);

            if (book == null)
                return new ApiResponse() { Code = "25", Description = "Book Does Not Exists", Data = null };


            return new ApiResponse() { Code = "00", Description = "Success", Data = book.Authors };

        }
        public async Task<ApiResponse> CreateBook(BookDto book)
        {
            var bookExists = _context.Book.Where((p) => (p.Title + p.Content).ToLower() == (book.Title + book.Content).ToLower()).Any();

            if (bookExists)
                return new ApiResponse() { Code = "25", Description = "Book Exists", Data = null };
            var authorList = new List<Author>();
            foreach (var id in book.AuthorIds)
            {
                var author = await _context.Author.FirstOrDefaultAsync(x => x.ID == id);
                if (author == null)
                    return new ApiResponse() { Code = "25", Description = "Author with id " + id + " does not exists", Data = book };

  authorList.Add(author);
            }
            var newBook = new Book()
            {
                Title = book.Title,
                Content = book.Content,
                Authors = authorList,

            };
            await _context.Book.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return new ApiResponse() { Code = "00", Description = "Success", Data = book };

        }

    }
}
