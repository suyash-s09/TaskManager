using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidReadWriteLibrary
{
    public class ValidReadWrite
    {
        public string ValidStringRead(string ReadMessage,string ErrorMessage)
        {
            Console.WriteLine(ReadMessage);
            string s = Console.ReadLine();
            if (s.Trim() != "") return s;
            Console.WriteLine(ErrorMessage);
            return "";
        }

        public int ValidIntRead(string ReadMessage, string ErrorMessage)
        {
            Console.WriteLine(ReadMessage);
            string s = Console.ReadLine();
            int readVal = -1;
            if (int.TryParse(s, out _))
                readVal = Convert.ToInt32(s);
            if (s.Trim() != "" && int.TryParse(s, out _)) return readVal;
            Console.WriteLine(ErrorMessage);
            return -1;
        }
    }
}
