using AdminLibrary;
using TaskLibrary;
using UserLibrary;
using System;
using System.Text.Json;
using Moq;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ValidReadWriteLibrary;
using System.Diagnostics;
using NUnit.Framework.Internal;
//using NUnit.Framework;



namespace TaskManagerTest
{
    
    [TestClass]
    public class AdminTests
    {
        string UserJsonFilePath = "C:/Users/developer/Desktop/TaskManger/TaskManagerTest/TestDatabase/users.json";
        string TaskJsonFilePath = "C:/Users/developer/Desktop/TaskManger/TaskManagerTest/TestDatabase/tasks.json";

        [TestMethod]
        public void Setup()
        {
            Tasks t = new Tasks();
            UserModel u = new UserModel();
            List<Tasks> TaskList = new List<Tasks>();
            List<UserModel> UserList = new List<UserModel>();

            
            File.WriteAllText(TaskJsonFilePath, string.Empty);

            
            File.WriteAllText(UserJsonFilePath, string.Empty);

            t.TaskId = 101;
            t.TaskName = "Test Preload";
            t.TaskDescription = "Test Task DescriptionPreload";
            t.StatusCompleted = false;
            t.UserId = 2;
            t.IsNew = false;
            TaskList.Add(t);

            u.UserId = 2;
            u.password = "p2";
            u.Update = false;
            UserList.Add(u);

            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(TaskJsonFilePath, jsonString);

            jsonString = JsonSerializer.Serialize<List<UserModel>>(UserList);
            File.WriteAllText(UserJsonFilePath, jsonString);

            
        }

        [TestMethod]
        public void ValidateAdminTest_ReturnsTrue() {
            Admin admin = new Admin();
            Assert.IsTrue(admin.validateAdmin("admin", "password"));
        }

        [TestMethod]
        public void ValidateAdminTest_ReturnsFalse()
        {
            Admin admin = new Admin();
            Assert.IsFalse(admin.validateAdmin("ad", "pa"));
        }

        
        [TestMethod]
        public void CreateUser_Creates()
        {
            Admin admin = new Admin();
            UserModel user = new UserModel();

            var validReadWriteMock = new Mock<ValidReadWrite>();
            var jsonFilePath = TaskJsonFilePath;
            var userJsonFilePath = UserJsonFilePath;
            Console.SetIn(new StringReader("1\np1\n"));

            user.UserId = 1;
            user.password = "p1";
            user.Update = false;
        
            NUnit.Framework.Assert.DoesNotThrow(() => admin.CreateUser(UserJsonFilePath));

            List<UserModel> userList = admin.ReadUser(userJsonFilePath);
            userList.Add(user);

            string jsonString = JsonSerializer.Serialize<List<UserModel>>(userList);
            File.WriteAllText(UserJsonFilePath, jsonString);

            userList = admin.ReadUser(userJsonFilePath);
            Console.WriteLine($"testing wla{userList[1].UserId}");

            // Assert           
            
            Assert.IsNotNull(userList);
            Assert.AreEqual(2, userList.Count);
            Assert.IsFalse(userList[userList.Count-1].Update);
        }

        [TestMethod]
        public void CreateTaskTest_Creates()
        {
            var admin = new Admin();
            var user = new User();
            var validReadWriteMock = new Mock<ValidReadWrite>();
            var jsonFilePath = TaskJsonFilePath;
            var userJsonFilePath = UserJsonFilePath;
            Console.SetIn(new StringReader("1\nTest\nTest Task Description\ninc\n1"));

            List<Tasks> TaskList = new List<Tasks>();
            TaskList = admin.ReadTask(TaskJsonFilePath);
            Tasks t = new Tasks();
            t.TaskId = 1;
            t.TaskName = "Test";
            t.TaskDescription = "Test Task Description";
            t.StatusCompleted = false;
            t.UserId = 1;
            t.IsNew = true;
            TaskList.Add(t);

            // Act
            NUnit.Framework.Assert.DoesNotThrow(() => admin.CreateTask(jsonFilePath, userJsonFilePath));
            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(TaskJsonFilePath, jsonString);

            // Assert
            var taskList = admin.ReadTask(jsonFilePath);
            Assert.IsNotNull(taskList);
            Assert.AreEqual(2, taskList.Count);

        }

        [TestMethod]
        public void ReadTask_ValidJsonFilePath_ReturnsTaskList()
        {
            FileStream fileStream = new FileStream(TaskJsonFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
            using StreamReader reader = new StreamReader(fileStream);
            var json = reader.ReadToEnd();
            List<Tasks> TaskList = new List<Tasks>();
            List<Tasks> tasks_list_read = JsonSerializer.Deserialize<List<Tasks>>(json);

            foreach (Tasks item in tasks_list_read)
            {
                if (item.UserId == 1)
                {
                    TaskList.Add(item);
                }
            }

            Assert.IsNotNull(TaskList);
            Assert.IsInstanceOfType<List<Tasks>>(TaskList);
            Assert.AreEqual(2, TaskList.Count);
        }

        [TestMethod]
        public void UpdateTaskTest()
        {
            var admin = new Admin();
            var user = new User();
            var validReadWriteMock = new Mock<ValidReadWrite>();
            var jsonFilePath = TaskJsonFilePath;
            var userJsonFilePath = UserJsonFilePath;
            Console.SetIn(new StringReader("1\n1\nTest\nTest Task Description\ninc\n1"));

            List<Tasks> TaskList = new List<Tasks>();
            TaskList = admin.ReadTask(TaskJsonFilePath);

            Tasks t = new Tasks();
            t.TaskId = 1;
            t.TaskName = "Test";
            t.TaskDescription = "Test Task Description";
            t.StatusCompleted = false;
            t.UserId = 1;
            t.IsNew = true;
            int idx = 0;
            for (int i = 0; i < TaskList.Count; i++)
            {
                if (TaskList[i].UserId == 1)
                {
                    idx = i;
                    break;
                }
            }
            TaskList[idx] = t;
            string jsonString = JsonSerializer.Serialize<List<Tasks>>(TaskList);
            File.WriteAllText(TaskJsonFilePath, jsonString);
            // Act
            NUnit.Framework.Assert.DoesNotThrow(() => admin.UpdateTask(jsonFilePath, userJsonFilePath));

            // Assert
            var taskList = admin.ReadTask(jsonFilePath);
            Assert.IsNotNull(taskList);
            Assert.AreEqual(2, taskList.Count);

            var userList = user.ReadUser(userJsonFilePath);
            Assert.IsNotNull(userList);
            Assert.AreEqual(2, userList.Count);
            Assert.IsTrue(userList[0].Update);
        }

    }
    
    [TestClass]
    public class UserTests
    {
        string UserJsonFilePath = "C:/Users/developer/Desktop/TaskManger/TaskManagerTest/TestDatabase/users.json";
        string TaskJsonFilePath = "C:/Users/developer/Desktop/TaskManger/TaskManagerTest/TestDatabase/tasks.json";

        [TestMethod]
        public void ValidateUser_ValidUser_ReturnsTrue()
        {
            var user = new User();
            bool result = user.ValidateUser(1, "p1", UserJsonFilePath);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUser_InvalidUser_ReturnsFalse()
        {
            var user = new User();
            bool result = user.ValidateUser(23, "WrongPassword", UserJsonFilePath);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReadUser_ValidJsonFilePath_ReturnsUserList()
        {
            FileStream fileStream = new FileStream(UserJsonFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
            using StreamReader reader = new StreamReader(fileStream);
            var json = reader.ReadToEnd();

            List<UserModel> UserList = new List<UserModel>();
            List<UserModel> users_list_read = JsonSerializer.Deserialize<List<UserModel>>(json);

            foreach (UserModel item in users_list_read.ToList())
            {
                UserList.Add(item);
            }

            Assert.IsNotNull(UserList);
            Assert.IsInstanceOfType<List<UserModel>>(UserList);
            Assert.AreEqual(1, UserList.Count);
        }

        [TestMethod]
        public void ReadTask_ValidJsonFilePath_ReturnsTaskList()
        {
            FileStream fileStream = new FileStream(TaskJsonFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
            using StreamReader reader = new StreamReader(fileStream);
            var json = reader.ReadToEnd();
            List<Tasks> TaskList = new List<Tasks>();
            List<Tasks> tasks_list_read = JsonSerializer.Deserialize<List<Tasks>>(json);

            foreach (Tasks item in tasks_list_read)
            {
                if (item.UserId == 1)
                {
                    TaskList.Add(item);
                }
            }

            Assert.IsNotNull(TaskList);
            Assert.IsInstanceOfType<List<Tasks>>(TaskList);
            Assert.AreEqual(1, TaskList.Count);
        }

        [TestMethod]
        public void PrintTasks_ValidJsonFilePath_PrintsTasks()
        {

        }

        [TestMethod]
        public void UpdateStatus_ValidJsonFilePath_UpdatesStatus()
        {

        }

        [TestMethod]
        public void ShowUpdate_ValidJsonFilePath_ShowsUpdates()
        {

        }

        [TestMethod]
        public void DismissUpdate_ValidJsonFilePath_DismissesUpdates()
        {

        }


    }
}