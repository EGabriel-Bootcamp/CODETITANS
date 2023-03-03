using CodeTitansBank;

string path = @"C:\Users\LENOVO\Documents\db.txt";


Helper.Welcome();

int answer;
Bank account;

do
{
	answer = Helper.WhatToDo();
	if (answer == 0) 
	{ 
		Environment.Exit(0);
	}
	else if (answer == 1)
	{
		account = SignUpLogin.SignUp(path);
		Helper.WhatToDoLogged(account);
	}
	else
	{
		SignUpLogin.Login();
	}

}while(answer != 0);

