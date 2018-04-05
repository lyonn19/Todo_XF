using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTasksPage : ContentPage
    {
        public EditTasksPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.TasksViewModel;
        }
    }
}