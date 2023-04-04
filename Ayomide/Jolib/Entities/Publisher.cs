using System.ComponentModel.DataAnnotations;

namespace Jolib.Entities
{
    public class Publisher
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public ICollection<Author> Authors { get; set; } = new List<Author>();

    }
}
