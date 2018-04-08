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
using MvvmHelpers;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    public class UpdateLogViewModel : ViewModelBase
    {

        public ObservableCollection<ItemUpdate> UpdateLogss;
        
        public UpdateLogViewModel()
        {
            UpdateLogss = new ObservableCollection<ItemUpdate>();
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
            {
                SelectedTodoItem = (TodoItem)navigationData;

            }
            
            return base.InitializeAsync(navigationData);
        }

        

        Command _updateLogsCommand;
        public Command EditTodoCommand
        {
            get
            {
                return _updateLogsCommand ?? (_updateLogsCommand = new Command(async () => await UpdateLogAsync(), () => !IsBusy));
            }
        }
        public async Task UpdateLogAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await GetUpdateLogs();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetUpdateLogs()
        {
            try
            {
                var result = await ItemUpdateDao.Instance.GetItemUpdateByIdAsync(SelectedTodoItem.Id);
                foreach (var item in result)
                {
                    UpdateLogss.Add(new ItemUpdate()
                    {
                        Id = item.Id,
                        UpdatedAt = item.UpdatedAt
                    });
                }

            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error getting TODO, try again", "OK");
            }
        }

        //private Task GetUpdateLogs
        //{
        //try
        //{
        //var result = await ItemUpdateDao.Instance.GetItemUpdateAsync(selete);
        //foreach (var item in result)
        // {
        //UpdateLogss.Add(new String
        //{
        //    item.
        //});
        //}

        //}
        //catch (Exception)
        //{
        //    await Application.Current.MainPage.DisplayAlert("Error", "Error getting TODO, try again", "OK");
        //}
        //}


    }
}
