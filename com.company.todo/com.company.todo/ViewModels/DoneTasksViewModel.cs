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

    }
}
