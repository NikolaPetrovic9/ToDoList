using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ToDoList.Model;
using ToDoList.View;
using ToDoList.ViewModel.Command;
using ToDoList.ViewModel.Helpers;

namespace ToDoList.ViewModel
{
    public class ToDoVM : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem>? ToDoItems { get; set; }
        private string newtoDoItem;
        public ICommand AddItemCommand { get; }
        public ICommand DelleteAllItemsCommand { get; }
        public ICommand DelleteAllDoneItemsCommand { get; }
        public ICommand DelleteItemCommand { get; }
        public ICommand EnableEditingCommand { get; }
        public ICommand ShowDoneItemsCommand { get; }
        public ICommand ShowNotDoneItemsCommand { get; }
        public ICommand ShowAllItemsCommand { get; }
        public ICommand IsDoneCommand { get; }
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
            ShowDoneItemsCommand = new RelayCommand(filterOnlyDoneItems);
            ShowNotDoneItemsCommand = new RelayCommand(filterOnlyNotDoneItems);
            ShowAllItemsCommand = new RelayCommand(showAllItems);
            DelleteItemCommand = new RelayCommand(deleteItem);
            EnableEditingCommand = new RelayCommand(enableEditItem);
            IsDoneCommand = new RelayCommand(isDoneChanged);
            foreach (var item in ToDoDatabase.ReadTable())
            {
                ToDoItems.Add(item);
            }
        }

        public void addNewItemToList(object param)
        {
            if (ToDoItems != null && !String.IsNullOrWhiteSpace(newtoDoItem))
            {
                ToDoItems.Clear();
                ToDoDatabase.AddNewItem(new ToDoItem { description = newtoDoItem, isDone = false });
                foreach (var item in ToDoDatabase.ReadTable())
                {
                    ToDoItems.Add(item);
                }
                NewToDoItem = String.Empty;
            }
        }
        public void isDoneChanged(object param)
        {
            if (ToDoItems != null)
            {
                ToDoItems.Clear();
                ToDoDatabase.UpdateItem((ToDoItem)param);
                foreach (var item in ToDoDatabase.ReadTable())
                {
                    ToDoItems.Add(item);
                }
            }
        }
        public void deleteAllItems(object param)
        {
            if (ToDoItems != null)
            {
                ToDoItems.Clear();
                ToDoDatabase.DeleteAllItems();
                foreach (var item in ToDoDatabase.ReadTable())
                {
                    ToDoItems.Add(item);
                }
            }
        }
        public void deleteAllDoneItems(object param)
        {
            if (ToDoItems != null)
            {
                ToDoItems.Clear();
                ToDoDatabase.DeleteAllDoneItems();
                foreach (var item in ToDoDatabase.ReadTable())
                {
                    ToDoItems.Add(item);
                }
            }
        }
        public void deleteItem(object param)
        {
            if (ToDoItems != null)
            {
                ToDoItems.Clear();
                ToDoDatabase.DeleteItem((ToDoItem)param);
                foreach (var item in ToDoDatabase.ReadTable())
                {
                    ToDoItems.Add(item);
                }
            }
        }
        public void enableEditItem(object param)
        {
            if (ToDoItems != null)
            {
                ToDoItems.Clear();
                ((ToDoItem)param).isReadOnly = !((ToDoItem)param).isReadOnly;
                ToDoDatabase.UpdateItem((ToDoItem)param);
               
                foreach (var item in ToDoDatabase.ReadTable())
                {
                    ToDoItems.Add(item);
                }
            }
        }
        //Filtering elements
        public void filterOnlyDoneItems(object param)
        {
            CollectionViewSource.GetDefaultView(ToDoItems).Filter = item => ((ToDoItem)item).isDone;
        }
        public void filterOnlyNotDoneItems(object param)
        {
            CollectionViewSource.GetDefaultView(ToDoItems).Filter = item => !((ToDoItem)item).isDone;
        }
        public void showAllItems(object param)
        {
            CollectionViewSource.GetDefaultView(ToDoItems).Filter = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
