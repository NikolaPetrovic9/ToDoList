using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Model;

namespace ToDoList.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        public event EventHandler? CanExecuteChanged;
        
        public RelayCommand(Action<object> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = execute;
        }


        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
