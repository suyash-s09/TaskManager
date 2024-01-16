
using System.Reflection.Metadata;
using AdminLibrary;
using UserLibrary;
using TaskLibrary;




User u1 = new User();
string jsonFilePath = "C:/Users/developer/Desktop/TaskManger/Database/tasks.json";

u1.SetUserId(101);
u1.SetUserPassword("password");
u1.printTasks(jsonFilePath);

Console.WriteLine("\n\nAfter Update\n\n");

u1.UpdateStatus(1, jsonFilePath);
u1.printTasks(jsonFilePath);
