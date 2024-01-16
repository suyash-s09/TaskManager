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
    public class User
    {
        private int userId;
        private string password;
        

        public void SetUserId(int userId)
        {
            this.userId = userId;
        }

        public void SetUserPassword(string password) {
            this.password = password;
        }

        public bool ValidateUser(int userId, string password)
        {
            return (this.userId==userId && this.password==password);
        }

       



    }
}
