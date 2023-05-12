using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CreateAdminDto
    {
        [Required]
        public string FirstName { get; set; } = default!;
         [Required]
        public string UserName { get; set; } = default!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = default!;
        }
}
