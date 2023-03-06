using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class ToDoItem
    {
        public int id { get; set; } 
        public string? description { get; set; }
        public bool isDone { get; set; }
    }
}
