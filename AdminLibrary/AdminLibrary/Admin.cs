using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
