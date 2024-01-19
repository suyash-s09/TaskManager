using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskLibrary;
using UserLibrary;
using ValidReadWriteLibrary;
using static System.Net.Mime.MediaTypeNames;

namespace AdminLibrary
{
    public class Admin
    {
        private string usename = "admin";
        private string password = "password";

        public bool validateAdmin(string username, string password)
        {
            return (this.usename == username && this.password == password);
        }

        public List<Tasks> ReadTask(string JsonFilePath)
        {
            using StreamReader reader = new(JsonFilePath);
            var json = reader.ReadToEnd();

            List<Tasks> TaskList = new List<Tasks>();
            List<Tasks> tasks_list_read = JsonSerializer.Deserialize<List<Tasks>>(json);

            foreach (Tasks item in tasks_list_read)
            {
                 TaskList.Add(item);
            }
            return TaskList;
        }

        public void printTasks(string JsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            foreach (Tasks item in TaskList)
            {
                Console.WriteLine($" TaskId: {item.TaskId} \n TaskName: {item.TaskName} \n TaskDescription: {item.TaskDescription} \n  UserId: {item.UserId}\n");
                if (item.StatusCompleted)
                {
                    Console.WriteLine("Status: Completed\n");
                }
                else Console.WriteLine("Status: Incomplete\n");
            }
        }

        public void CreateTask(string JsonFilePath,string UserJsonFilePath)
        {
            ValidReadWrite ValidReader = new ValidReadWrite();
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            Tasks t = new Tasks();

            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(UserJsonFilePath);

            t.TaskId = ValidReader.ValidIntRead("Enter the Task Id: ", "Enter a valid input ! TaskId has to be a non-negative integer");

            while (t.TaskId == -1)
            {
                t.TaskId = ValidReader.ValidIntRead("Enter the Task Id: ", "Enter a valid input ! TaskId has to be a non-negative integer");
            }

            bool found = false;
            foreach (Tasks item in TaskList)
            {
                if (item.TaskId == t.TaskId)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Console.WriteLine($"The Task with TaskId : {t.TaskId} already exits!!...");
                return;
            }

            t.TaskName = ValidReader.ValidStringRead("Enter the Task Name: ", "Enter a valid input !");
            while (t.TaskName == "")
            {
                t.TaskName = ValidReader.ValidStringRead("Enter the Task Name: ", "Enter a valid input !");                
            }

            t.TaskDescription = ValidReader.ValidStringRead("Enter the Task Description: ", "Enter a valid input !");
            while (t.TaskDescription == "")
            {
                t.TaskDescription = ValidReader.ValidStringRead("Enter the Task Description: ", "Enter a valid input !");                
            }

            while (true)
            {
                Console.WriteLine("Enter the Task Complete Status -> \n c : complete \n inc: incomplete ");
                string s = Console.ReadLine();
                bool validInput = true;
                switch (s)
                {
                    case "c":t.StatusCompleted = true; break;
                    case "inc":t.StatusCompleted = false; break;
                    default: validInput=false;break;
                }
                if (validInput) break;
                Console.WriteLine("Enter a valid input !");
            }
            int index = 0;
            bool foundUser = false;

            while (true)
            {
                Console.WriteLine("Enter the UserId to which task is assigned: ");
                string s= Console.ReadLine();
                index = 0;
                foundUser = false;
                if (int.TryParse(s, out _))
                {
                    t.UserId = Convert.ToInt32(s);
                    foreach (UserModel item in UserList.ToList())
                    {
                        if (item.UserId == t.UserId)
                        {
                            foundUser = true;
                            break;
                        }
                        else index++;
                    }
                }
                    
                if (s.Trim() != "" && int.TryParse(s, out _) && foundUser) break;
                if (s.Trim() != "" && int.TryParse(s, out _) && !foundUser) Console.WriteLine($"User with userId: {t.UserId} does not exist!");
                
                Console.WriteLine("Enter a valid input !");
            }
            
            t.IsNew = true;
            TaskList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);     
            
            UserList[index].Update = true;
            jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(UserJsonFilePath, jsonString);

            Console.WriteLine($"The Task with TaskId : {t.TaskId} successfully created!!...");
        }

        public void UpdateTask( string JsonFilePath , string UserJsonFilePath)
        {
            ValidReadWrite ValidReader = new ValidReadWrite();
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(UserJsonFilePath);

            int Userindex = 0;
            bool foundUser = false;

            int taskId = -1;
            while (taskId == -1)
            {
                taskId = ValidReader.ValidIntRead("Enter the TaskId to Update:", "Enter a valid input !");           
            }
            
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);

            bool found = false;
            int index = 0;

            foreach (Tasks item in TaskList.ToList())
            {
                if (item.TaskId == taskId)
                {
                    found = true;
                    break;
                }
                else index++;
            }

            if (!found)
            {
                Console.WriteLine($"The Task with TaskId: {taskId} does not exist.");
                return;
            }

            TaskList[index].TaskName = ValidReader.ValidStringRead("Update the Task Name: ", "ENter a valid input!");
            while (TaskList[index].TaskName == "")
            {
                TaskList[index].TaskName = ValidReader.ValidStringRead("Update the Task Name: ", "ENter a valid input!");                
            }

            TaskList[index].TaskDescription = ValidReader.ValidStringRead("Update the Task Description: ", "ENter a valid input!");
            while (TaskList[index].TaskDescription == "")
            {
                TaskList[index].TaskDescription = ValidReader.ValidStringRead("Update the Task Description: ", "ENter a valid input!");                
            }

            while (true)
            {
                Console.WriteLine("Update the Task Complete Status -> \n c : complete \n inc: incomplete ");
                string s = Console.ReadLine();
                bool validInput = true;
                switch (s)
                {
                    case "c": TaskList[index].StatusCompleted = true; break;
                    case "inc": TaskList[index].StatusCompleted = false; break;
                    default: validInput = false; break;
                }
                if (validInput) break;
                Console.WriteLine("Enter a valid input !");
            }

            while (true)
            {
                Userindex = 0;
                foundUser = false;

                Console.WriteLine("Enter the UserId to which task is assigned: ");
                string s = Console.ReadLine();
                if (int.TryParse(s, out _))
                {
                    TaskList[index].UserId = Convert.ToInt32(s);
                    foreach (UserModel item in UserList.ToList())
                    {
                        if (item.UserId == TaskList[index].UserId)
                        {
                            foundUser = true;
                            break;
                        }
                        else Userindex++;
                    }
                }

                if (s.Trim() != "" && int.TryParse(s, out _) && foundUser) break;
                if (s.Trim() != "" && int.TryParse(s, out _) && !foundUser) Console.WriteLine($"User with userId: {TaskList[index].UserId} does not exist!");

                Console.WriteLine("Enter a valid input !");
            }          

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);

            Console.WriteLine($"The Task with TaskId: {taskId} successfully updated.\n");
        }

        public void DeleteTask( string JsonFilePath)
        {
            ValidReadWrite ValidReader = new ValidReadWrite();
            int taskId = -1;
            while (taskId == -1)
            {
                taskId = ValidReader.ValidIntRead("Enter the TaskId to Delete:", "Enter a valid input!");                
            }
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);

            bool found = false;
            
            foreach (Tasks item in TaskList.ToList())
            {
                if (item.TaskId == taskId)
                {
                    found = true;
                    TaskList.Remove(item);
                }
            }

            if (!found)
            {
                Console.WriteLine($"The Task with TaskId: {taskId} does not exist.");
                return;
            }
            Console.WriteLine($"The Task with TaskId: {taskId} successfully deleted.");
            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);

            Console.WriteLine($"The Task with TaskId: {taskId} successfully deleted!.\n");
        }

        //-------------------------------------------------------------------------------------------------------------------------

        public List<UserModel> ReadUser(string JsonFilePath)
        {
            using StreamReader reader = new(JsonFilePath);
            var json = reader.ReadToEnd();
            List<UserModel> UserList = new List<UserModel>();

            List<UserModel> users_list_read = JsonSerializer.Deserialize<List<UserModel>>(json);

            foreach (UserModel item in users_list_read.ToList())
            {
                UserList.Add(item);
            }
            return UserList;
        }

        public void printUsers(string JsonFilePath)
        {
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);
            foreach (UserModel item in UserList)
            {
                Console.WriteLine($" UserId: {item.UserId} \n User Password: {item.password} \n ");
            }
        }

        public void CreateUser(string JsonFilePath)
        {
            ValidReadWrite ValidReader = new ValidReadWrite();
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);
            UserModel t = new UserModel();
            t.UserId = ValidReader.ValidIntRead("Enter the UserId: ", "Enter a valid input!");
            while (t.UserId == -1)
            {
                t.UserId = ValidReader.ValidIntRead("Enter the UserId: ", "Enter a valid input!");
            }           

            bool found = false;
            foreach (UserModel item in UserList.ToList())
            {
                if (item.UserId == t.UserId)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Console.WriteLine($"The User with UserId : {t.UserId} already exits!!...");
                return;
            }

            t.password = ValidReader.ValidStringRead("Enter the password: ", "Enter a valid input!");
            while (t.password == "")
            {
                t.password = ValidReader.ValidStringRead("Enter the password: ", "Enter a valid input!");
            }          

            UserList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);

            Console.WriteLine($"The User with UserId: {t.UserId} successfully created.\n");
        }

        public void DeleteUser(string JsonFilePath)
        {
            ValidReadWrite ValidReader = new ValidReadWrite();
            int userId = -1;
            while (userId == -1)
            {
                userId = ValidReader.ValidIntRead("Enter the UserId to Delete:", "Enter a valid input!");
            }
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);

            bool found = false;

            foreach (UserModel item in UserList.ToList())
            {
                if (item.UserId == userId)
                {
                    found = true;
                    UserList.Remove(item);
                }
            }

            if (!found)
            {
                Console.WriteLine($"The User with UserId: {userId} does not exist.");
                return;
            }

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);

            Console.WriteLine($"The User with UserId: {userId} successfully deleted.\n");
        }

        public void UpdateUser(string JsonFilePath)
        {
            ValidReadWrite ValidReader = new ValidReadWrite();
            int userId = -1;
            while (userId == -1)
            {
                userId = ValidReader.ValidIntRead("Enter the UserId to Update:", "Enter a valid input!");
            }
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);

            bool found = false;
            int index = 0;

            foreach (UserModel item in UserList.ToList())
            {
                if (item.UserId == userId)
                {
                    found = true;
                    break;
                }
                else index++;
            }

            if (!found)
            {
                Console.WriteLine($"The User with UserId: {userId} does not exist.");
                return;
            }

            UserList[index].UserId = ValidReader.ValidIntRead("Update the UserId: ", "Enter a valid input!");
            while (UserList[index].UserId == -1)
            {
                UserList[index].UserId = ValidReader.ValidIntRead("Update the UserId: ", "Enter a valid input!");
            }

            UserList[index].password = ValidReader.ValidStringRead("Update the User password: ", "Enter a valid input!");
            while (UserList[index].password == "")
            {
                UserList[index].password = ValidReader.ValidStringRead("Update the User password: ", "Enter a valid input!");
            }           

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);

            Console.WriteLine($"The User with UserId: {userId} successfully updated!\n");
        }
    }
}
