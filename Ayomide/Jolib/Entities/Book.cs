using System.ComponentModel.DataAnnotations;

namespace Jolib.Entities
{
    public class Book
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public ICollection<Author> Authors { get; set; } = new List<Author>();

    }
}
