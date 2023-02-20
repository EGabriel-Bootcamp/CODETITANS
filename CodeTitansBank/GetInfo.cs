using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTitansBank
{
	public static class GetInfo
	{
		internal static string GetEmail()
		{
			string email;

			Console.WriteLine("Please Enter your Email");

			do
			{
				email = Console.ReadLine();
				if (email == null || email.Trim() == "")
				{
					Console.WriteLine("Please Enter email");
				}

				if (!email.Contains('@'))
				{
					Console.WriteLine("Please Enter a valid Email");
				}
			} while (email == null || !email.Contains('@'));

			return email;
		}

		internal static string GetPassword()
		{
			string password;

			Console.WriteLine("Enter your Password: ");

			do
			{
				password = Console.ReadLine();

				if (password == null || password.Length < 4)
				{
					Console.WriteLine("Please enter a password with at least four characters");
				}
			} while (password == null || password.Length < 4);


			return password;
		}

		internal static string GetPhoneNo()
		{
			string phoneNo;
			bool isDigit;

			Console.WriteLine("Please Enter your phone number");

			do
			{
				phoneNo = Console.ReadLine();
				isDigit = int.TryParse(phoneNo, out int phone);
				if (isDigit == false)
				{
					Console.WriteLine("Please enter a valid number");
				}

			} while (isDigit == false);

			return phoneNo;

		}

		internal static string GetUsername()
		{
			string username;
			bool valid = false;

			Console.WriteLine("Please Enter your User Name");

			do
			{
				username = Console.ReadLine();
				if (username == null || username.Trim() == "")
				{
					Console.WriteLine("Please Enter User Name");
				}

				if (username.Length < 3 || username.Trim() == "")
				{
					Console.WriteLine("Please Enter a username that is at least 4 characters");
				}
				else
				{
					valid = true;
				}
			} while (!valid);

			return username;
		}
	}
}
