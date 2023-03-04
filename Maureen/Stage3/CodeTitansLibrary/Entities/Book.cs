using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTitansLibrary.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public decimal Price { get; set; }


        //Relationships
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [ValidateNever]
        public Author Author { get; set; }
    }
}
