using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {

        public TasksViewModel()
        {

        }

        /// <summary>
        /// Init ViewModel
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
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

    }
}
