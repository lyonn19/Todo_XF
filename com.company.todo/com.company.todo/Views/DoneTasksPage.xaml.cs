using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.ViewModels;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoneTasksPage : ContentPage
    {
        public DoneTasksPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.TasksViewModel;
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}