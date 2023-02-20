using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTitansBank
{
	public static class Helper
	{
		public static void Welcome()
		{
			Console.WriteLine("*********************************************");
			Console.WriteLine("       Welcome to CodeTitans Bank            ");
			Console.WriteLine("*********************************************");

		}

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
			Console.WriteLine();

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

		public static List<string> GetUserDetails(string path, int index)
		{
			List<string> list = new();


			string[] readlines = File.ReadAllLines(path);

			foreach (string line in readlines)
			{
				list.Add(line.Split(new char[] { ',' })[index]);
			}
			return list;
		}

		public static void PrintMessage(string message)
		{
			Console.WriteLine(message);
		}

		internal static void LoginScreen()
		{
			throw new NotImplementedException();
		}
	}
}
