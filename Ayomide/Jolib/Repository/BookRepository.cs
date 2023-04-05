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
        private  List<Book> GetBooks()
        {
          return  _context.Book.Include((b) => b.Authors).ToList();  
            
        }
        public ApiResponse GetAllBooks()
        {
            var books = GetBooks();
            if (books == null)
            {
                return new ApiResponse() { Code = "25", Description = "No Books record found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = books };
        }

        public ApiResponse GetBook(int id)
        {
            var book =  GetBooks().FirstOrDefault(x => x.ID == id);
            if (book == null)
            {
                return new ApiResponse() { Code = "25", Description = "book record not found", Data = null };
            }
            return new ApiResponse() { Code = "00", Description = "Success", Data = book };

        }
        public  ApiResponse GetAuthorsAttachedToBook(int id)
        {
            var book = GetBooks().FirstOrDefault(x => x.ID == id);

            if (book == null)
                return new ApiResponse() { Code = "25", Description = "Book Does Not Exists", Data = null };


            return new ApiResponse() { Code = "00", Description = "Success", Data = book.Authors };

        }
        public async Task<ApiResponse> CreateBook(BookDto book)
        {
            var bookExists = GetBooks().Where((p) => (p.Title + p.Content).ToLower() == (book.Title + book.Content).ToLower()).Any();

            if (bookExists)
                return new ApiResponse() { Code = "25", Description = "Book Exists", Data = null };
            var authorList = new List<Author>();
            foreach (var id in book.AuthorIds)
            {
                var author = await _context.Author.Include
                    ((b) => b.Books).Include
                    ((b)=> b.Publisher).FirstOrDefaultAsync(x => x.ID == id);
                if (author == null)
                    return new ApiResponse() { Code = "25", Description = "Author with id " + id + " does not exists", Data = null };

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
            return new ApiResponse() { Code = "00", Description = "Success", Data = newBook };

        }

    }
}
