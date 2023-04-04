using Jolib.Entities;
using System.ComponentModel.DataAnnotations;

namespace Jolib.Dtos
{
    public class BookDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public List<long> AuthorIds { get; set; } = new List<long>();
    }
}
