
using System.Reflection.Metadata;
using AdminLibrary;
using UserLibrary;
using TaskLibrary;

//string TaskjsonFilePath = "../../../../../Database/tasks.json";


string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
string sFile = System.IO.Path.Combine(sCurrentDirectory, @"../../../../Database/tasks.json");
string TaskjsonFilePath = Path.GetFullPath(sFile);

sFile = System.IO.Path.Combine(sCurrentDirectory, @"../../../../Database/users.json");

string UserjsonFilePath = Path.GetFullPath(sFile);

void AdminUI()
{
    Admin admin = new Admin();
    string username = "";
    string password = "";

    while (true)
    {
        Console.WriteLine("Enter Admin username: ");
        username = Console.ReadLine();

        if (username.Trim() != "") break;
        Console.WriteLine("Enter a valid input ! Admin Username cannot be empty!!");

    }

    while (true)
    {
        Console.WriteLine("Enter Admin password: ");
        password = Console.ReadLine();
        if (password.Trim() != "") break;
        Console.WriteLine("Enter a valid input ! Password cannot be empty!!");
    }



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
            case "1":admin.CreateTask(TaskjsonFilePath,UserjsonFilePath);
                break;

            case "2":
                admin.printTasks(TaskjsonFilePath);
                break;

            case "3":
                admin.UpdateTask(TaskjsonFilePath,UserjsonFilePath);
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
    int userId = -1;
    string password = "";
    while (true)
    {
        Console.WriteLine("Enter UserId: ");
        string s = Console.ReadLine();
        if(int.TryParse(s, out _))
            userId = Convert.ToInt32(s);

        if (s.Trim() != "" && int.TryParse(s, out _)) break;
        Console.WriteLine("Enter a valid input !");
    }

    while (true)
    {
        Console.WriteLine("Enter password: ");
        password = Console.ReadLine();
        if (password.Trim() != "") break;
        Console.WriteLine("Enter a valid input ! Password cannot be empty!!");
    }


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

    while (user.Update)
    {
        Console.WriteLine("You have been assigned new Tasks !");
        Console.WriteLine("show: Show the new tasks");
        Console.WriteLine("dismiss: close the notification");

        string showOrDismiss = Console.ReadLine();
        switch(showOrDismiss)
        {
            case "show":user.showUpdate(TaskjsonFilePath,UserjsonFilePath);
                break;

            case "dismiss":user.dismissUpdate(TaskjsonFilePath, UserjsonFilePath);
                break;

            default: Console.WriteLine("Enter a valid input");
                break;
        }
    }

    while (true)
    {
        Console.WriteLine("\n");
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


