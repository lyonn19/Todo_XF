using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    public class DetailTodoViewModel : ViewModelBase
    {
        
        public DetailTodoViewModel()
        {
            SelectedTodoItem = new TodoItem();
        }

        private TodoItem _selectedTodoItem;
        public TodoItem SelectedTodoItem
        {
            get { return _selectedTodoItem; }
            set
            {
                _selectedTodoItem = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
                SelectedTodoItem = (TodoItem)navigationData;
            return base.InitializeAsync(navigationData);
        }
    }
}
