using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Domain
{
	public class UserUpdateDTO
	{
		//[Required]
		//public int Id { get; set; }
		[Required]
		public string MarritalStatus { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
	}
}
