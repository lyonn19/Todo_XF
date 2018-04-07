using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using com.company.todo.Control;
using com.company.todo.Data.Local;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace com.company.todo.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {

        public ObservableCollection<ViewTask> PendingTasks { get; set; }
        
        public TasksViewModel()
        {
            PendingTasks = new ObservableCollection<ViewTask>();
        }

        
        /// <summary>
        /// Init ViewModel
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public override Task InitializeAsync(object navigationData)
        {
            //TodoTaskCommand.Execute(null);
            return Task.FromResult(false);
            //return base.InitializeAsync(navigationData);
        }

        private async Task GetTodoTask()
        {
            try
            {
                var result = await TaskDao.Instance.GetTasksAsync();
                foreach (var item in result)
                {
                    PendingTasks.Add(new ViewTask
                    {
                        Imagen = ImageSource.FromStream(() => new MemoryStream(item.Imagen)),
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

        public ICommand FlipCommand => new Command(Flip);
        private void Flip()
        {
            MessagingCenter.Send(this, AppSettings.TransitionMessage, TransitionType.Flip);
        }

        public ICommand NavigateToAddTask
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.NavigateToAsync<AddTasksViewModel>();
                });
            }
        }

        Command _todoTaskCommand;
        public Command TodoTaskCommand
        {
            get
            {
                return _todoTaskCommand ?? (_todoTaskCommand = new Command(async () => await GetTodoTaskAsync(), () => !IsBusy));
            }
        }
        public async Task GetTodoTaskAsync()
        {
            if (IsBusy)
                return;
            try
            {
                PendingTasks.Clear();
                IsBusy = true;
                await GetTodoTask();
            }
            finally
            {
                IsBusy = false;
            }
        }

        
    }
    
    public class ViewTask
    {
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public bool Status { get; set; }

        public ImageSource Imagen { get; set; }
    }
    
}
