
using System.Reflection.Metadata;
using UserLibrary;


User u1 = new User();
string jsonFilePath = "C:/Users/developer/Desktop/TaskManger/Database/tasks.json";

u1.SetUserId(101);
u1.SetUserPassword("password");
u1.printTasks(jsonFilePath);



