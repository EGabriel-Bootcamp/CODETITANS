using System.ComponentModel.DataAnnotations;

namespace CodeTitansLibrary.Entities.DTO
{
    public class PublisherDTO
    {
        [Required]
        public string PublisherName { get; set; }
    }
}
