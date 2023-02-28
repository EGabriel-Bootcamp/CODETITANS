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
		
		public static Bank SignUp(string path)
		{			
			userName = GetInfo.GetUsername();
			password = GetInfo.GetPassword();
			email = GetInfo.GetEmail();
			age = GetInfo.GetAge();
			phoneNo = GetInfo.GetPhoneNo();
			var account = new Bank(userName, email, age, phoneNo);



			using (var sw = File.AppendText(path))
			{
				sw.WriteLine($"{userName},{password},{email},{age},{phoneNo},{account.AccountNumber},{account.Balance}");
			}


			Helper.PrintMessage("\n*****************************************");
			Helper.PrintMessage("Registration Successful!");
			Helper.PrintMessage($"What would you like to do?");
			Helper.PrintMessage("*****************************************\n");


			return account;
		}

		public static void Login()
		{
			string path = @"C:\Users\LENOVO\Documents\db.txt";

			string username;
			string password;
			bool keepGoing = true;

			//Get List of Usernames and passwords

			string[] allLines;
			List<string> usernames = new List<string>();
			List<string> passwords = new List<string>();
			List<string> emails = new List<string>();
			List<int> Ages = new List<int>();
			List<string> phoneNos = new List<string>();

			allLines = File.ReadAllLines(path);

			foreach (var line in allLines)
			{
				string[] eachline = line.Split(new char[] { ',' });
				usernames.Add(eachline[0]);
				passwords.Add(eachline[1]);
				emails.Add(eachline[2]);
				Ages.Add(Int32.Parse(eachline[3]));
				phoneNos.Add(eachline[4]);
			}


			do
			{
				int trackIndex;
				username = GetInfo.GetUsername();
				password = GetInfo.GetPassword();

				
				for (int i = 0; i < usernames.Count; i++)
				{
					if (usernames[i] == username && passwords[i] == password)
					{
						trackIndex = i;
						int trials = 1;
						//password = GetInfo.GetPassword();
						while (passwords[i] != password)
						{
							if (trials >= 3)
							{
								Console.WriteLine("Maximum of three trials reached. App will exit");
								Environment.Exit(0);
							}
							Console.WriteLine("Invalid password, Try again: ");
							password = GetInfo.GetPassword();
							trials++;
						}
						Bank account = new(username, emails[trackIndex], Ages[trackIndex], phoneNos[trackIndex]);
						Helper.WhatToDoLogged(account);
						keepGoing = false;

					}
				}
				Console.WriteLine("Invalid User, Try again: ");

			} while (keepGoing);
		}

	}

}
