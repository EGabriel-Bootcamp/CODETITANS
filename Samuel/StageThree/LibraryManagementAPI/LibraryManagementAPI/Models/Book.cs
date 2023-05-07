using LibraryManagementAPI.DataContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }



        //Relationship
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }


}