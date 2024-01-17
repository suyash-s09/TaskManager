using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskLibrary;
using UserLibrary;

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

            Console.WriteLine("Enter the Task Id: ");
            t.TaskId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the Task Name: ");
            t.TaskName = Console.ReadLine();

            Console.WriteLine("Enter the Task Description: ");
            t.TaskDescription = Console.ReadLine();

            Console.WriteLine("Enter the Task Status: ");
            t.StatusCompleted = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Enter the UserId to which task is assigned: ");
            t.UserId = Convert.ToInt32(Console.ReadLine());

            TaskList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        public void UpdateTask( string JsonFilePath)
        {
            Console.WriteLine("Enter the TaskId to Update:");
            int taskId = Convert.ToInt32(Console.ReadLine());
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

            Console.WriteLine("Update the Task Name: ");
            TaskList[index].TaskName = Console.ReadLine();

            Console.WriteLine("Update the Task Description: ");
            TaskList[index].TaskDescription = Console.ReadLine();

            Console.WriteLine("Update the Task Status: ");
            TaskList[index].StatusCompleted = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Update the UserId to which task is assigned: ");
            TaskList[index].UserId = Convert.ToInt32(Console.ReadLine());


            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);


        }

        public void DeleteTask( string JsonFilePath)
        {
            Console.WriteLine("Enter the TaskId to Delete:");
            int taskId = Convert.ToInt32(Console.ReadLine());
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

            Console.WriteLine("Enter the UserId: ");
            t.UserId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the password: ");
            t.password = Console.ReadLine();

            UserList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        public void DeleteUser(string JsonFilePath)
        {
            Console.WriteLine("Enter the UserId to Delete:");
            int userId = Convert.ToInt32(Console.ReadLine());
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
            Console.WriteLine("Enter the UserId to Update:");
            int userId = Convert.ToInt32(Console.ReadLine());
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

            Console.WriteLine("Update the UserId: ");
            UserList[index].UserId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Update the User password: ");
            UserList[index].password = Console.ReadLine();

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

    }
}
