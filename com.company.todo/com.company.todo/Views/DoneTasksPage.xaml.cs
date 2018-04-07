using System;
using System.Collections;
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
            BindingContext = ViewModelLocator.Instance.Resolve<DoneTasksViewModel>();
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        /// <summary>
        /// Event OnSearchButtonPressed search task on search pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchText = SeachBarDoneTasks.Text.ToLower();
            var result =
                ViewModelLocator.Instance.Resolve<TasksViewModel>()
                    .PendingTasks.Where(x => x.Content.ToLower().Contains(searchText));
            var enumerable = result as ViewTask[] ?? result.ToArray();
            var any = enumerable.Any();
            ListViewTasks.ItemsSource = any ? (IEnumerable) enumerable : new List<string>() {"Tasks Not Found"};
        }


        /// <summary>
        /// Event OnTextChanged search task on text change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ListViewTasks.ItemsSource = ViewModelLocator.Instance.Resolve<TasksViewModel>().PendingTasks;
            }
            else
            {
                var searchText = SeachBarDoneTasks.Text.ToLower();
                var result = ViewModelLocator.Instance.Resolve<TasksViewModel>().PendingTasks.Where(x => x.Content.ToLower().Contains(searchText));
                var enumerable = result as ViewTask[] ?? result.ToArray();
                var any = enumerable.Any();
                ListViewTasks.ItemsSource = any ? (IEnumerable)enumerable : new List<string>() { "Tasks Not Found" };
            }
        }
    }
}