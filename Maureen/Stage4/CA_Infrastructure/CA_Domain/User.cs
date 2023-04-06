using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Domain
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public string Gender { get; set; }
		[Required]
		public string MarritalStatus { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
	}
}
