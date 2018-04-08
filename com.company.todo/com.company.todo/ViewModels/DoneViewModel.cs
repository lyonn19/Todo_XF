using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.Data.Local;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    /// <summary>
    /// ViewModel for DoneTodoPage shows all done todoItem
    /// </summary>
    class DoneViewModel : ViewModelBase
    {
        #region Fields
        public ObservableCollection<TodoItem> DoneItems { get; set; }
        #endregion

        #region Builder
        public DoneViewModel()
        {
            DoneItems = new ObservableCollection<TodoItem>();
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
            DoneCommand.Execute(null);
            return base.InitializeAsync(navigationData);
        }

        /// <summary>
        /// Get done todoItems from local database
        /// </summary>
        /// <returns></returns>
        private async Task GetDone()
        {
            try
            {
                var result = await TodoDao.Instance.GetDoneAsync();
                foreach (var item in result)
                {
                    DoneItems.Add(new TodoItem
                    {
                        Id = item.Id,
                        ImagenSource = ImageSource.FromStream(() => new MemoryStream(item.Imagen)),
                        Content = item.Content,
                        CreatedAt = item.CreatedAt,
                        Status = item.Status,
                        UpdatedAt = item.UpdatedAt
                    });
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error saving task, try again", "OK");
            }
        }

        /// <summary>
        /// Delete todoItem from local database
        /// </summary>
        /// <returns></returns>
        private async Task DeleteTodo()
        {
            try
            {
                await TodoDao.Instance.DeleteTodoAsync(SelectedTodoItem);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error occured", "OK");
            }
        }
        #endregion
        
        #region Commands
        public ICommand NavigateToDetail
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.NavigateToAsync<DetailTodoViewModel>(SelectedTodoItem);
                });
            }
        }
        public ICommand NavigateToEdit
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.NavigateToAsync<EditTodoViewModel>(SelectedTodoItem);
                });
            }
        }

        Command _doneCommand;
        public Command DoneCommand
        {
            get
            {
                return _doneCommand ?? (_doneCommand = new Command(async () => await GetDoneAsync(), () => !IsBusy));
            }
        }
        public async Task GetDoneAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                DoneItems.Clear();
                await GetDone();
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        Command _deleteTodoCommand;
        public Command DeleteTodoCommand
        {
            get
            {
                return _deleteTodoCommand ?? (_deleteTodoCommand = new Command(async () => await DeleteTodoAsync(), () => !IsBusy));
            }
        }
        public async Task DeleteTodoAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await DeleteTodo();
            }
            finally
            {
                IsBusy = false;
                DoneCommand.Execute(null);
            }
        }
        #endregion
    }
}
