using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.TimeZoneInfo;

namespace CodeTitansBank
{
	public static class Helper
	{

		// Welcome message
		public static void Welcome()
		{
			Console.WriteLine("*********************************************");
			Console.WriteLine("       Welcome to CodeTitans Bank            ");
			Console.WriteLine("*********************************************");

		}


		//Promts the user to choose which operation user would like to perform
		public static int WhatToDo()
		{
			int answer;
			bool isDigit = false;
			string enteredtext;

			Console.Write("1 \t Open Account");
			Console.WriteLine();
			Console.Write("2 \t Login to Account");
			Console.WriteLine();
			Console.Write("0 \t Exit");
			Console.WriteLine("\n");

			do
			{
				enteredtext = Console.ReadLine();

				while (enteredtext == null)
				{
					Console.WriteLine("Please enter a number");
					enteredtext = Console.ReadLine();
				}

				isDigit = int.TryParse(enteredtext, out answer);

				if (!isDigit)
				{
					Console.WriteLine("Please enter a valid number");
				}
				else if (answer != 1 && answer != 2 && answer != 0)
				{
					Console.WriteLine("Please enter 0 or 1 or 2");
					isDigit = false;
				}

			} while (!isDigit);

			return answer;
		}


		//Prints message to screen
		public static void PrintMessage(string message)
		{
			Console.WriteLine(message);
		}



		//Logged in helpers
		//prompts the user to choose an operation when user is logged in.
		public static int WhatToDoLogged(Bank account)
		{
			int answer;
			bool isDigit = false;
			string enteredtext;

			Console.Write("1 \t Check Balance");
			Console.WriteLine();
			Console.Write("2 \t Make Deposit");
			Console.WriteLine();
			Console.Write("3 \t Make Withdrawal");
            Console.WriteLine();
            Console.Write("4 \t Logout");
            Console.WriteLine("\n");

			do
			{
				
				enteredtext = Console.ReadLine();

				while (enteredtext == null)
				{
					Console.WriteLine("Please enter a number");
					enteredtext = Console.ReadLine();
				}

				isDigit = int.TryParse(enteredtext, out answer);

				if (!isDigit)
				{
					Console.WriteLine("Please enter a valid number");
				}
				else if (answer != 1 && answer != 2 && answer != 3 && answer != 4)
				{
					Console.WriteLine("Please enter 1 or 2 or 3 or 4");
					isDigit = false;
				}

			} while (!isDigit);




			//Actions to take after user chooses what to do.
			if (answer == 1)
			{
				decimal balance = account.CheckBalance();
				Console.WriteLine($"Your account balance is {balance}\n");
				Helper.WhatToDoLogged(account);
			}
			else if (answer == 2)
			{
				decimal amount = GetInfo.GetDepositAmount();
				decimal balance = account.Deposit(amount);
				DateTime transactionTime = DateTime.UtcNow;


                Console.WriteLine($"\nYou have successfully deposited {amount}");
				Console.WriteLine($"Your new balance is {balance}\n");

				Console.ReadKey();


                Console.WriteLine("======= Transaction Summary ===========\n");
                Console.WriteLine("Transaction Date =====> {0} \t Amount Deposited =========> {1} \t Balance =======> {2}\n", transactionTime, amount, balance);

                Helper.WhatToDoLogged(account);
			}
			else if (answer == 3)
			{
				decimal amount = GetInfo.GetWithdrawalAmount();
				decimal balance = account.Withdraw(amount);
                DateTime transactionTime = DateTime.UtcNow;

                Console.WriteLine($"\nYou have successfully withdrawn {amount}");
				Console.WriteLine($"Your new balance is {balance}\n");


                Console.WriteLine("======= Transaction Summary ===========\n");
                Console.WriteLine("Transaction Date =====> {0} \t Amount Withdrawn =========> {1} \t Balance =======> {2} \n", transactionTime, amount, balance);

                Helper.WhatToDoLogged(account);
			}
			else if(answer == 4)
			{
				SignUpLogin.Logout(account);
			}

			return answer;
		}


		//Gives an error message when user withraws above balance.
		internal static void ErrorWithdraw(Bank account)
		{
			Console.WriteLine("Insufficient Balance");
			Console.WriteLine($"Your current balance is {account.Balance}\n");


			Helper.WhatToDoLogged(account);
		}

    }
}
