using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskmanagerback.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title{ get; set; }
        public string Content { get; set; }

        
        public virtual ItemsList ItemsList { get; set; }


    }
}
