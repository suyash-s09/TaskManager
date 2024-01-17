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
                Console.WriteLine($" TaskId: {item.TaskId} \n TaskName: {item.TaskName} \n TaskDescription: {item.TaskDescription} \n Status: {item.StatusCompleted} UserId: {item.UserId}\n");
            }
        }

        public void CreateTask(string JsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            Tasks t = new Tasks();

            while (true)
            {
                Console.WriteLine("Enter the Task Id: ");
                string s = Console.ReadLine();
                if(int.TryParse(s, out _))
                t.TaskId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input !");
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

            while (true)
            {
                Console.WriteLine("Enter the Task Name: ");
                t.TaskName = Console.ReadLine();
                if (t.TaskName.Trim() != "") break;
                Console.WriteLine("Enter a valid input !");
            }

            while (true)
            {
                Console.WriteLine("Enter the Task Description: ");
                t.TaskDescription = Console.ReadLine();
                if (t.TaskDescription.Trim() != "") break;
                Console.WriteLine("Enter a valid input !");
            }

            while (true)
            {
                Console.WriteLine("Enter the Task Status: ");
                string s = Console.ReadLine();
                if (bool.TryParse(s, out _))
                    t.StatusCompleted = Convert.ToBoolean(s);
                if (s.Trim() != "" && bool.TryParse(s,out _)) break;
                Console.WriteLine("Enter a valid input !");
            }

            while (true)
            {
                Console.WriteLine("Enter the UserId to which task is assigned: ");
                string s= Console.ReadLine();
                if (int.TryParse(s, out _))
                    t.UserId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input !");
            }
            

            TaskList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        public void UpdateTask( string JsonFilePath)
        {
            int taskId = -1;
            while (true)
            {
                Console.WriteLine("Enter the TaskId to Update:");
                string s = Console.ReadLine() ;
                if (int.TryParse(s, out _))
                    taskId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input !");
            }
            
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);

            bool found = false;
            int index = 0;

            foreach (Tasks item in TaskList)
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

            while (true)
            {
                Console.WriteLine("Update the Task Name: ");
                TaskList[index].TaskName = Console.ReadLine();
                if (TaskList[index].TaskName.Trim() != "") break;
                Console.WriteLine("ENter a valid input!");
            }

            while (true)
            {
                Console.WriteLine("Update the Task Description: ");
                TaskList[index].TaskDescription = Console.ReadLine();
                if (TaskList[index].TaskDescription.Trim() != "") break;
                Console.WriteLine("ENter a valid input!");
            }

            while (true)
            {
                Console.WriteLine("Update the Task Status: ");
                string s = Console.ReadLine();
                if (bool.TryParse(s, out _))
                    TaskList[index].StatusCompleted = Convert.ToBoolean(s);
                if (s.Trim() != "" && bool.TryParse(s, out _)) break;
                Console.WriteLine("ENter a valid input!");
            }

            while (true)
            {
                Console.WriteLine("Update the UserId to which task is assigned: ");
                string s= Console.ReadLine();
                if (int.TryParse(s, out _))
                    TaskList[index].UserId = Convert.ToInt32(s);
                if(s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("ENter a valid input!");
            }
            


            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);


        }

        public void DeleteTask( string JsonFilePath)
        {
            int taskId = -1;
            while (true)
            {
                Console.WriteLine("Enter the TaskId to Delete:");
                string s = Console.ReadLine();
                if (int.TryParse(s, out _))
                    taskId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input!");
            }
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);

            bool found = false;
            
            foreach (Tasks item in TaskList)
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

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        //-------------------------------------------------------------------------------------------------------------------------

        public List<UserModel> ReadUser(string JsonFilePath)
        {
            using StreamReader reader = new(JsonFilePath);
            var json = reader.ReadToEnd();
            List<UserModel> UserList = new List<UserModel>();

            List<UserModel> users_list_read = JsonSerializer.Deserialize<List<UserModel>>(json);

            foreach (UserModel item in users_list_read)
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
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);
            UserModel t = new UserModel();

            while (true)
            {
                Console.WriteLine("Enter the UserId: ");
                string s = Console.ReadLine();
                if (int.TryParse(s, out _))
                    t.UserId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input!");
            }
            

            bool found = false;
            foreach (UserModel item in UserList)
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

            while (true)
            {
                Console.WriteLine("Enter the password: ");
                t.password = Console.ReadLine();
                if (t.password.Trim() != "") break;
                Console.WriteLine("Enter a valid input!");
            }
            

            UserList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        public void DeleteUser(string JsonFilePath)
        {
            int userId = -1;
            while (true)
            {
                Console.WriteLine("Enter the UserId to Delete:");
                string s = Console.ReadLine();
                if (int.TryParse(s, out _))
                    userId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input!");
            }
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);

            bool found = false;

            foreach (UserModel item in UserList)
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
        }

        public void UpdateUser(string JsonFilePath)
        {
            int userId = -1;
            while (true)
            {
                Console.WriteLine("Enter the UserId to Update:");
                string s = Console.ReadLine();
                if (int.TryParse(s, out _))
                    userId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input!");
            }
            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(JsonFilePath);

            bool found = false;
            int index = 0;

            foreach (UserModel item in UserList)
            {
                if (item.UserId == userId)
                {
                    found = true;
                }
                else index++;
            }

            if (!found)
            {
                Console.WriteLine($"The User with UserId: {userId} does not exist.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Update the UserId: ");
                string s = Console.ReadLine();
                UserList[index].UserId = Convert.ToInt32(s);
                if (s.Trim() != "" && int.TryParse(s, out _)) break;
                Console.WriteLine("Enter a valid input!");
            }

            while (true)
            {
                Console.WriteLine("Update the User password: ");
                UserList[index].password = Console.ReadLine();
                if (UserList[index].password.Trim() != "") break;
                Console.WriteLine("Enter a valid input!");
            }
            

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

    }
}
