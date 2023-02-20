using CodeTitansBank;

string path = @"C:\codeTitansBank\db.txt";


Helper.Welcome();

int answer;

do
{
	answer = Helper.WhatToDo();
	if (answer == 0) 
	{ 
		Environment.Exit(0);
	}
	else if (answer == 1)
	{
		SignUpLogin.SignUp(path);
	}
	else
	{
		SignUpLogin.Login();
	}

}while(answer != 0);

