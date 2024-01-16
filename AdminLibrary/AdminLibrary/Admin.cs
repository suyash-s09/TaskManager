using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskLibrary;

namespace AdminLibrary
{
    class Admin
    {
        string usename = "admin";
        string password = "password";

        bool validateAdmin(string username, string password)
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
                Console.WriteLine($" TaskId: {item.TaskId} \n TaskName: {item.TaskName} \n TaskDescription: {item.TaskDescription} \n Status: {item.StatusCompleted}");
            }
        }

        public void CreateTask(Tasks t, string JsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            TaskList.Add(t);

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

        public void UpdateStatus(int taskId, string JsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            foreach (Tasks item in TaskList)
            {
                if (item.TaskId == taskId)
                {
                    item.StatusCompleted = true;
                }
            }

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);


        }

        public void DeleteTask(int taskId, string JsonFilePath)
        {
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = ReadTask(JsonFilePath);
            
            foreach (Tasks item in TaskList)
            {
                if (item.TaskId == taskId)
                {
                    TaskList.Remove(item);
                }
            }

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(JsonFilePath, jsonString);
        }

    }
}
