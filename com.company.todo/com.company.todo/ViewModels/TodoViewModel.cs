using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using com.company.todo.Data.Local;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace com.company.todo.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoViewModel : ViewModelBase
    {
        #region Fields
        public ObservableCollection<TodoItem> TodoItems { get; set; }
        #endregion

        #region Builder
        public TodoViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            SelectedTodoItem = new TodoItem();
        }
        #endregion

        #region Properties
        private TodoItem _selectedTask;
        public TodoItem SelectedTodoItem
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
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
            return Task.FromResult(false);
            //return base.InitializeAsync(navigationData);
        }

        /// <summary>
        /// Get todos todoItems from local database
        /// </summary>
        /// <returns></returns>
        private async Task GetTodo()
        {
            try
            {
                var result = await TodoDao.Instance.GetTodoAsync();
                foreach (var item in result)
                {
                    TodoItems.Add(new TodoItem
                    {
                        Id = item.Id,
                        ImagenSource = ImageSource.FromStream(() => new MemoryStream(item.Imagen)),
                        Imagen = item.Imagen,
                        Content = item.Content,
                        CreatedAt = item.CreatedAt,
                        Status = item.Status,
                        UpdatedAt = item.UpdatedAt
                    });
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message+ "Error getting TODO, try again", "OK");
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
                throw;
            }
        }
        #endregion

        #region Command
        public ICommand NavigateToNewTodo
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.NavigateToAsync<AddTodoViewModel>();
                });
            }
        }
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

        Command _todoCommand;
        public Command TodoCommand
        {
            get
            {
                return _todoCommand ?? (_todoCommand = new Command(async () => await GetTodoAsync(), () => !IsBusy));
            }
        }
        public async Task GetTodoAsync()
        {
            if (IsBusy)
                return;
            try
            {
                TodoItems.Clear();
                IsBusy = true;
                await GetTodo();
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
                TodoCommand.Execute(null);
            }
        }
        #endregion

    }
}
