using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskmanagerback.Models
{
    public class ItemsList
    {
        public int ItemsListId { get; set; }
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
