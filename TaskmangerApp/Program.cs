﻿
using System.Reflection.Metadata;
using AdminLibrary;
using UserLibrary;
using TaskLibrary;

string TaskjsonFilePath = "C:/Users/developer/Desktop/TaskManger/Database/tasks.json";
string UserjsonFilePath = "C:/Users/developer/Desktop/TaskManger/Database/users.json";

void AdminUI()
{
    Admin admin = new Admin();


    Console.WriteLine("Enter Admin username: ");
    string username = Console.ReadLine();

    Console.WriteLine("Enter Admin password: ");
    string password = Console.ReadLine();

    if (!admin.validateAdmin(username, password))
    {        
        Console.WriteLine("OOps ! Wrong Credentials ... ");

        invalidResponse:
        Console.WriteLine("r: Retry Login");
        Console.WriteLine("b: Go Back to Dashboard");

        string response = Console.ReadLine();
        if (response == "r") AdminUI();
        else if (response == "b") DashBoard();
        else goto invalidResponse;
    }

    while(true){
        Console.WriteLine("1: Create Task");
        Console.WriteLine("2: Read Task");
        Console.WriteLine("3: Update Task");
        Console.WriteLine("4: Delete Task");
        Console.WriteLine("5: Create User");
        Console.WriteLine("6: Read User");
        Console.WriteLine("7: Update User");
        Console.WriteLine("8: Delete User");
        Console.WriteLine("exit: To Exit");

        string input = Console.ReadLine();
        if (input == "exit") break;

        switch (input)
        {
            case "1":admin.CreateTask(TaskjsonFilePath);
                break;

            case "2":
                admin.printTasks(TaskjsonFilePath);
                break;

            case "3":
                admin.UpdateTask(TaskjsonFilePath);
                break;

            case "4":
                admin.DeleteTask(TaskjsonFilePath);
                break;

            case "5":
                admin.CreateUser(UserjsonFilePath);
                break;

            case "6":
                admin.printUsers(UserjsonFilePath);
                break;

            case "7":
                admin.UpdateUser(UserjsonFilePath);
                break;

            case "8":
                admin.DeleteUser(UserjsonFilePath);
                break;

            default:
                Console.WriteLine("Enter a valid Input. ");
                break;
        }

       
    }
}

void UserUI()
{
    User user = new User();

    Console.WriteLine("Enter UserId: ");
    int userId = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter password: ");
    string password = Console.ReadLine();

    if (!user.ValidateUser(userId, password, UserjsonFilePath))
    {
        Console.WriteLine("OOps ! Wrong Credentials ... ");

        invalidUserResponse:
        Console.WriteLine("r: Retry Login");
        Console.WriteLine("b: Go Back to Dashboard");

        string response = Console.ReadLine();
        if (response == "r")UserUI();
        else if (response == "b") DashBoard();
        else goto invalidUserResponse;
    }

    user.SetUserId(userId);
    user.SetUserPassword(password);

    while (true)
    {
        Console.WriteLine("1: Read Task");
        Console.WriteLine("2: Update Task Status");
        Console.WriteLine("exit: To Exit");

        string input = Console.ReadLine();
        if (input == "exit") break;

        switch (input)
        {
            case "1":user.printTasks(TaskjsonFilePath);
                break;

            case "2":user.UpdateStatus(TaskjsonFilePath);
                break;

            default:
                Console.WriteLine("Enter a valid Input. ");
                break;
        }
    }

    
}

void DashBoard()
{
    Console.WriteLine("\t\t\t***********************************************************************");
    Console.WriteLine("\t\t\t                       WELCOME TO TaskManager                       ");
    Console.WriteLine("\t\t\t***********************************************************************");

    invalidAdminOrUser:

    Console.WriteLine("a= Admin");
    Console.WriteLine("u= User");

    Console.Write("Enter the choice: ");

    string adminOrUser = Console.ReadLine();
    if (adminOrUser == "a")
    {
        AdminUI();
    }

    else if (adminOrUser == "u")
    {
        UserUI();
    }
    else {
        Console.WriteLine("Enter a valid input.");
        goto invalidAdminOrUser; 
    }

}

DashBoard();


