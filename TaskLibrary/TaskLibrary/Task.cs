using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary
{
    public class Tasks
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public bool StatusCompleted { get; set; } = false;
        public bool IsNew { get; set; }
        public int UserId { get; set; }


    }
}
