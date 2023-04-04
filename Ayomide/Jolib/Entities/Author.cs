using System.ComponentModel.DataAnnotations;

namespace Jolib.Entities
{
    public class Author
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public Publisher Publisher { get; set; } = new Publisher();
       
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
