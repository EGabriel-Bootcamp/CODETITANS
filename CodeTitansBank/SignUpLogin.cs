using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTitansBank
{
	public static class SignUpLogin
	{
		static string userName;
		static string password;
		static string email;
		static string phoneNo;
		static int age;
		public static void SignUp(string path)
		{			
			userName = GetInfo.GetUsername();
			password = GetInfo.GetPassword();
			email = GetInfo.GetEmail();
			phoneNo = GetInfo.GetPhoneNo();


			using (var sw = File.AppendText(path))
			{
				sw.WriteLine($"{userName},{password},{email},{phoneNo}");
			}
			Helper.PrintMessage("\n*****************************************");
			Helper.PrintMessage("Registration Successful!\nWhat do you want to do? ");
			Helper.PrintMessage("*****************************************\n");
		}

		internal static void Login()
		{
			string path = @"C:\codeTitansBank\db.txt";

			string username;
			string password;
			bool keepGoing = true;

			//Get List of Usernames and passwords
			List<string> usernames = Helper.GetUserDetails(path, 0);
			List<string> passwords = Helper.GetUserDetails(path, 1);

			do
			{
				username = GetInfo.GetUsername();
				for (int i = 0; i < usernames.Count - 1; i++)
				{
					if (usernames[i] == username)
					{
						password = GetInfo.GetPassword();
						while (passwords[i] != password)
						{
							Console.WriteLine("Invalid password, Try again: ");
							password = GetInfo.GetPassword();
						}

						Helper.LoginScreen();
						keepGoing = false;
					}
					else
					{
						Console.WriteLine("Invalid User, Try again: ");
					}
				}
			} while (keepGoing);
		}
	}
}
