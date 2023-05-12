using System.ComponentModel.DataAnnotations;

namespace Jolib.Dtos
{
    public class PublisherDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
