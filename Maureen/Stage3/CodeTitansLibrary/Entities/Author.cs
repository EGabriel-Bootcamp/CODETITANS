using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTitansLibrary.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Biography { get; set; }


        //Relationships
        [Required]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        [ValidateNever]
        public Publisher Publisher { get; set; }
    }
}
