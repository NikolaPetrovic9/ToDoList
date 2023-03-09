using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.ViewModel.Helpers
{
    public static class ToDoDatabase
    {
        private static string _databasePath  = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ToDoList.db");
        public static void AddNewItem(ToDoItem item)
        {
            using(var connection = new SQLiteConnection(_databasePath))
            {
                connection.CreateTable<ToDoItem>();
                connection.Insert(item);
            }
        }
        public static void UpdateItem(ToDoItem item)
        {
            using (var connection = new SQLiteConnection(_databasePath))
            {
                connection.CreateTable<ToDoItem>();
                connection.Update(item);
            }
        }
        public static void DeleteItem(ToDoItem item)
        {
            using (var connection = new SQLiteConnection(_databasePath))
            {
                connection.CreateTable<ToDoItem>();
                connection.Delete(item);
            }
        }
        public static void DeleteAllItems()
        {
            using (var connection = new SQLiteConnection(_databasePath))
            {
                connection.CreateTable<ToDoItem>();
                connection.DeleteAll<ToDoItem>();
            }
        }
        public static void DeleteAllDoneItems()
        {
            using (var connection = new SQLiteConnection(_databasePath))
            {
                connection.CreateTable<ToDoItem>();
                connection.Execute("DELETE FROM ToDoItem WHERE isDone = TRUE");
            }
        }
        public static ObservableCollection<ToDoItem> ReadTable()
        {
            var toDoItems = new List<ToDoItem>();
            using (var connection = new SQLiteConnection(_databasePath))
            {
                connection.CreateTable<ToDoItem>();
                toDoItems = connection.Table<ToDoItem>().ToList();          
            }
            var toDoObservable = new ObservableCollection<ToDoItem>(toDoItems);
            return toDoObservable;
        }
    }
}
