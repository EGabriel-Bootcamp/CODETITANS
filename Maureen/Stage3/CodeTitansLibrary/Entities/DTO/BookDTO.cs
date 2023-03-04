using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTitansLibrary.Entities.DTO
{
    public class BookDTO
    {
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public decimal Price { get; set; }


        //Relationships
        [Required]
        public int AuthorId { get; set; }
    }
}
