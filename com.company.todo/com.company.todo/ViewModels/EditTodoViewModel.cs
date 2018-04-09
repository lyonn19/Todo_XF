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
    /// <summary>
    /// ViewModel for EditTodoPage edit todoItem
    /// </summary>
    public class EditTodoViewModel : ViewModelBase
    {
        #region Builder
        public EditTodoViewModel()
        {
            SelectedTodoItem = new TodoItem();
        }
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        /// <summary>
        /// Init ViewModel
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
                SelectedTodoItem = (TodoItem)navigationData;
            return base.InitializeAsync(navigationData);
        }
        /// <summary>
        /// Edit todoItem from local database
        /// </summary>
        /// <returns></returns>
        private async Task EditTodo()
        {
            try
            {
                SelectedTodoItem.UpdatedAt = DateTime.Now;
                await TodoDao.Instance.EditTodoAsync(SelectedTodoItem);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error occured", "OK");
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
