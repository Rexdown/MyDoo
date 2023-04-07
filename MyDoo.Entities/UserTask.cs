using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoo.Entities
{
    public class UserTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public TaskType Type { get; set; } 
        public bool Important { get; set; }
        public bool Complete { get; set; }

        //public virtual User User { get; set; }
    }
}
