using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    public class ToDoVM : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem>? ToDoItems { get; set; }
        public ToDoVM() 
        {
            ToDoItems = new ObservableCollection<ToDoItem>();
            //if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            //{
                ToDoItems.Add(new ToDoItem { description = "Test4", isDone = false, id = 1 });
                ToDoItems.Add(new ToDoItem { description = "Test2", isDone = false, id = 1 });
                ToDoItems.Add(new ToDoItem { description = "Test3", isDone = false, id = 1 });
           // }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
