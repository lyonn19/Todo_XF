using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Models;
using com.company.todo.ViewModels;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoneTodoPage : ContentPage
    {
        public DoneTodoPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<DoneViewModel>();
            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android 8.0 where the search bar would be hidden
                SeachBarDoneTasks.HeightRequest = 40.0;
            }
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
                ViewModelLocator.Instance.Resolve<TodoViewModel>()
                    .TodoItems.Where(x => x.Content.ToLower().Contains(searchText));
            var enumerable = result as TodoItem[] ?? result.ToArray();
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
                ListViewTasks.ItemsSource = ViewModelLocator.Instance.Resolve<DoneViewModel>().DoneItems;
            }
            else
            {
                var searchText = SeachBarDoneTasks.Text.ToLower();
                var result = ViewModelLocator.Instance.Resolve<DoneViewModel>().DoneItems.Where(x => x.Content.ToLower().Contains(searchText));
                var enumerable = result as TodoItem[] ?? result.ToArray();
                var any = enumerable.Any();
                ListViewTasks.ItemsSource = any ? (IEnumerable)enumerable : new List<string>() { "Tasks Not Found" };
            }
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var task = ((ListView)sender).SelectedItem as TodoItem;
            if (task == null) return;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().SelectedTodoItem = task;
            ListViewTasks.SelectedItem = null;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().NavigateToDetail.Execute(null);
        }

        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Edit Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}