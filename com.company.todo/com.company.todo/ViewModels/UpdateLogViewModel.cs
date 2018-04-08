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
        #region Fields
        public ObservableCollection<ItemUpdate> UpdateLogs { get; set; }
        #endregion

        #region Builder
        public UpdateLogViewModel()
        {
            UpdateLogs = new ObservableCollection<ItemUpdate>();
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

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
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
            {
                SelectedTodoItem = (TodoItem)navigationData;
                EditTodoCommand.Execute(null);
            }
            return base.InitializeAsync(navigationData);
        }
        
        /// <summary>
        /// Get UpdateLog from local databse by Itemtodo Id
        /// </summary>
        /// <returns></returns>
        private async Task GetUpdateLogs()
        {
            try
            {
                var result = await ItemUpdateDao.Instance.GetItemUpdateByIdAsync(SelectedTodoItem.Id);
                var itemUpdates = result as ItemUpdate[] ?? result.ToArray();
                foreach (var item in itemUpdates)
                {
                    UpdateLogs.Add(new ItemUpdate()
                    {
                        Id = item.Id,
                        UpdatedAt = item.UpdatedAt
                    });
                }
                Count = itemUpdates.Count();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error getting TODO, try again", "OK");
            }
        }
        #endregion

        #region Command
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
                UpdateLogs.Clear();
                await GetUpdateLogs();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
