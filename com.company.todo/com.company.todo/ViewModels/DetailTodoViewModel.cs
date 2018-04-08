using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.Models;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{

    /// <summary>
    /// ViewModel for DetailTodoPage show todoItem detail
    /// </summary>
    public class DetailTodoViewModel : ViewModelBase
    {
        #region Builder
        public DetailTodoViewModel()
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
        #endregion

        #region Commands
        /// <summary>
        /// NavigateTo UpdateLogs
        /// </summary>
        public ICommand NavigateToUpdateLog
        {
            get 
            {
                return new Command(async () =>
                {
                    await Navigation.NavigateToAsync<UpdateLogViewModel>(SelectedTodoItem);
                });
            }
        }
        #endregion
    }
}
