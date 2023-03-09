using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class ToDoItem 
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; } 
        public string? description { get; set; }
        public bool isDone { get; set; }
        public bool isReadOnly { get; set; } = true;
    }
}
