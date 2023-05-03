using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Application.Features.Queries.GetAllUsers
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
