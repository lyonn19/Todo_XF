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
    public partial class TodoTasksPage : ContentPage
    {
        public TodoTasksPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<TasksViewModel>();
        }


        protected override void OnAppearing()
        {
            ViewModelLocator.Instance.Resolve<TasksViewModel>().TodoTaskCommand.Execute(null);
            base.OnAppearing();

        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}