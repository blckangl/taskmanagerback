using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace taskmanagerback.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
