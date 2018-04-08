using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Data.Local;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    public class EditTodoViewModel : ViewModelBase
    {
        public EditTodoViewModel()
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

        #region Funcions
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
                SelectedTodoItem = (TodoItem)navigationData;
            return base.InitializeAsync(navigationData);
        }

        private async Task EditTodo()
        {
            try
            {
                await TodoDao.Instance.EditTodoAsync(SelectedTodoItem);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error occured", "OK");
                throw;
            }
        }
        #endregion

        #region Commnands
        Command _editTodoCommand;
        public Command EditTodoCommand
        {
            get
            {
                return _editTodoCommand ?? (_editTodoCommand = new Command(async () => await EditTodoAsync(), () => !IsBusy));
            }
        }
        public async Task EditTodoAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await EditTodo();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
