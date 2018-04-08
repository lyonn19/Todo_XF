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
    class DoneViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> DoneItems { get; set; }

        public DoneViewModel()
        {
            DoneItems = new ObservableCollection<TodoItem>();
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

        #region Funtions
        public override Task InitializeAsync(object navigationData)
        {
            DoneCommand.Execute(null);
            return base.InitializeAsync(navigationData);
        }

        private async Task GetDoneTask()
        {
            try
            {
                var result = await TodoDao.Instance.GetTodoAsync();
                foreach (var item in result)
                {
                    DoneItems.Add(new TodoItem()
                    {
                        ImagenSource = ImageSource.FromStream(() => new MemoryStream(item.Imagen)),
                        Content = item.Content,
                        CreatedAt = item.CreatedAt,
                        Status = item.Status,
                        UpdateAt = item.UpdateAt
                    });
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error saving task, try again", "OK");
            }
        }

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
                await GetDoneTask();
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
