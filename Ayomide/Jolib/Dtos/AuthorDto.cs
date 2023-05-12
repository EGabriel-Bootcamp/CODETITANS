using System.ComponentModel.DataAnnotations;

namespace Jolib.Dtos
{
    public class AuthorDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public long PublisherId { get; set; }= long.MinValue;
    }
}
