using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.Data.Local;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        private ObservableCollection<Models.Task> PendingTasks;

        private ObservableCollection<Models.Task> DoneTasks;


        public TasksViewModel()
        {
            PendingTasks = new ObservableCollection<Models.Task>();
            DoneTasks = new ObservableCollection<Models.Task>();
        }

        private int _countTask;
        public int CountTask {
            get { return _countTask; }
            set
            {
                _countTask = value;
                OnPropertyChanged();
            }
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
                    PendingTasks.Add(item);
                }

                CountTask = PendingTasks.Count;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error saving task, try again", "OK");
            }
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
                IsBusy = true;
                await GetTodoTask();
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
