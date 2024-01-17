using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using TaskLibrary;


namespace UserLibrary
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string password { get; set; }
        public bool Update {  get; set; }
    }
    public class User : UserModel
    {
        
        public void SetUserId(int userId)
        {
            this.UserId = userId;
        }

        public void SetUserPassword(string password)
        {
            this.password = password;
        }
        

        public bool ValidateUser(int userId, string password, string JsonFilePath)
        {
            using StreamReader reader = new(JsonFilePath);
            var json = reader.ReadToEnd();
            List<UserModel> users_list_read = JsonSerializer.Deserialize<List<UserModel>>(json);

            foreach(UserModel u in users_list_read.ToList())
            {
                if(u.UserId == userId && u.password==password)
                {
                    this.Update = u.Update;
                    this.UserId = userId;
                    this.password=password;
                    return true;
                }
            }
            return false;
        }

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

        public List<Tasks> ReadTask(string JsonFilePath)
        {


            using StreamReader reader = new(JsonFilePath);
            var json = reader.ReadToEnd();
            List<Tasks> TaskList = new List<Tasks>();

            List<Tasks> tasks_list_read = JsonSerializer.Deserialize<List<Tasks>>(json);



            foreach (Tasks item in tasks_list_read)
            {
                if (item.UserId == this.UserId)
                {
                    TaskList.Add(item);
                }
            }



            return TaskList;

        }

        public void printTasks(string JsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            foreach (Tasks item in TaskList)
            {
                Console.WriteLine("\n");
                Console.WriteLine($" TaskId: {item.TaskId} \n TaskName: {item.TaskName} \n TaskDescription: {item.TaskDescription} \n Status: {item.StatusCompleted}");
            }
        }

        public void UpdateStatus(string JsonFilePath)
        {
            int taskId = -1;
            while (true)
            {
                Console.WriteLine("Enter the TaskId to Update:");
                string s = Console.ReadLine();
                if(int.TryParse(s, out _))
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
                    item.StatusCompleted = true;
                }
            }
            if (!found)
            {
                Console.WriteLine($"The Task with TaskId: {taskId} does not exist.");
                return;
            }

            Console.WriteLine($"Status of Task with Task Id: {taskId} Successfully Updated!!");

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        public void showUpdate(string TaskJsonFilePath,string UserJsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(TaskJsonFilePath);

            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(UserJsonFilePath);


            foreach (Tasks item in TaskList)
            {
                if (!item.IsNew) continue;
                item.IsNew = false;
                Console.WriteLine("\n");
                Console.WriteLine($" TaskId: {item.TaskId} \n TaskName: {item.TaskName} \n TaskDescription: {item.TaskDescription} \n Status: {item.StatusCompleted}");
            }

            foreach(UserModel item in UserList)
            {
                if(this.UserId == item.UserId)
                {
                    item.Update = false;
                }
            }
            this.Update = false;

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(TaskJsonFilePath, jsonString);

            jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(UserJsonFilePath, jsonString);
        }

        public void dismissUpdate(string JsonFilePath,string UserJsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);

            List<UserModel> UserList = new List<UserModel>();
            UserList = ReadUser(UserJsonFilePath);
            foreach (Tasks item in TaskList)
            {
                if (!item.IsNew) continue;
                item.IsNew = false;
            }

            foreach (UserModel item in UserList)
            {
                if (this.UserId == item.UserId)
                {
                    item.Update = false;
                }
            }
            this.Update = false;

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);

            jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(UserJsonFilePath, jsonString);

        }






    }
}
