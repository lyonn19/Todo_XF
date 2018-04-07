using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.Data.Local;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    class DoneTasksViewModel : ViewModelBase
    {
        public ObservableCollection<ViewTask> DoneTasks { get; set; }

        public DoneTasksViewModel()
        {
            DoneTasks = new ObservableCollection<ViewTask>();
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            DoneTaskCommand.Execute(null);
            return base.InitializeAsync(navigationData);
        }

        private async Task GetDoneTask()
        {
            try
            {
                var result = await TaskDao.Instance.GetTasksAsync();
                foreach (var item in result)
                {
                    DoneTasks.Add(new ViewTask()
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

        

        Command _doneTaskCommand;
        public Command DoneTaskCommand
        {
            get
            {
                return _doneTaskCommand ?? (_doneTaskCommand = new Command(async () => await GetDoneTaskAsync(), () => !IsBusy));
            }
        }
        public async Task GetDoneTaskAsync()
        {
            if (IsBusy)
                return;
            try
            {
                DoneTasks.Clear();
                IsBusy = true;
                await GetDoneTask();
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command _searchDoneTasksCommand;
        public Command SearchDoneTasksCommand
        {
            get
            {
                return _searchDoneTasksCommand ?? (_searchDoneTasksCommand = new Command(async () => await SeachDoneTasksAsync(), () => !IsBusy));
            }
        }
        public async Task SeachDoneTasksAsync()
        {
            if (IsBusy)
                return;
            try
            {
                //PendingTasks.Clear();
                IsBusy = true;
                SearchTodoTask();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SearchTodoTask()
        {
            try
            {
                DoneTasks.Where(x => x.Content.ToLower().Contains(SearchText.ToLower()));
                //var enumerable = result as ViewTask[] ?? result.ToArray();
                //var any = enumerable.Any();
                //ListViewTasks.ItemsSource = any ? (IEnumerable)enumerable : new List<string>() { "Task Not found" };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
