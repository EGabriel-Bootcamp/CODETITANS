using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace CodeTitansBank
{
	public class Bank
	{
		//Seed account number from where others would be created
		private static int defaultNumber = 12345678;


		//Account Properties;
		public string AccountNumber { get; set; }
		public decimal Balance { get; private set; } 

		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public int Age { get; set; }
		public string Password { get; set; }

		public Bank(string username, string email, int age, string phoneNo, decimal balance=0)
		{
			//Populating the account number
			this.AccountNumber = defaultNumber.ToString();
			defaultNumber++;

			this.Balance = balance;

			//populating user info;
			this.UserName = username;
			this.Email = email;
			this.Age = age;
			this.PhoneNumber = phoneNo;
		}

		public decimal Deposit(decimal amount)
		{
			Balance += amount;

			return Balance;
		}

		public decimal Withdraw(decimal amount)
		{
			if (Balance >= amount)
			{
				Balance -= amount;
			}
			else
			{
				Helper.ErrorWithdraw(this);
			}

			return Balance;
		}
		public decimal CheckBalance()
		{
			return Balance;
		}

	}
}
