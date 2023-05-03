using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Application.Features.Commands.EditUser
{
    public class EditUserDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
