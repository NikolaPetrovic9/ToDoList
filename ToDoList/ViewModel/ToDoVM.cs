using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    public class ToDoVM : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem>? ToDoItems { get; set; }
        private string newtoDoItem;

        public ICommand AddItemCommand { get; }
        public ICommand DelleteAllItemsCommand { get; }
        public ICommand DelleteAllDoneItemsCommand { get; }

        public string NewToDoItem
        {
            get
            { return newtoDoItem; }
            set
            {
                newtoDoItem = value;
                OnPropertyChanged(nameof(newtoDoItem));
            }
        }

        public ToDoVM()
        {
            ToDoItems = new ObservableCollection<ToDoItem>();
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                ToDoItems.Add(new ToDoItem { description = "Test4", isDone = false, id = 1 });
                ToDoItems.Add(new ToDoItem { description = "Test2", isDone = false, id = 1 });
                ToDoItems.Add(new ToDoItem { description = "Test3", isDone = false, id = 1 });
            }
            AddItemCommand = new RelayCommand(addNewItemToList);
            DelleteAllItemsCommand = new RelayCommand(deleteAllItems);
            DelleteAllDoneItemsCommand = new RelayCommand(deleteAllDoneItems);
        }

        public void addNewItemToList()
        {
            if(ToDoItems != null && !String.IsNullOrWhiteSpace(newtoDoItem))
            {
                ToDoItems.Add(new ToDoItem { description = newtoDoItem, isDone = false, id = 1 });
                NewToDoItem = String.Empty;
            }
        }
        public void deleteAllItems()
        {
            if (ToDoItems != null)
            {
                ToDoItems.Clear();
            }
        }
        public void deleteAllDoneItems()
        {
            if (ToDoItems != null)
            {
                foreach (var item in ToDoItems.ToList())
                {
                    if (item.isDone)
                    {
                        ToDoItems.Remove(item);
                    }
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
