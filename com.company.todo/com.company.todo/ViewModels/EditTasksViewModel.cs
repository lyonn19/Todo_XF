using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.ViewModels.Base;

namespace com.company.todo.ViewModels
{
    public class EditTasksViewModel : ViewModelBase
    {
        private Models.Task _selectedTask;
        private Models.Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public EditTasksViewModel()
        {
            SelectedTask = new Models.Task();
        }

        public override Task InitializeAsync(object navigationData)
        {
            // Todo use object navigation data 
            return base.InitializeAsync(navigationData);
        }
    }
}
