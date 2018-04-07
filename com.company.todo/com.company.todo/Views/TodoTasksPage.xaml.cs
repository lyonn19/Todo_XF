using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Control;
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
            if (Device.RuntimePlatform == Device.Android)
            {
                //Fixes an android 8.0 where the search bar would be hidden
                SeachBarTodoTasks.HeightRequest = 40.0;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModelLocator.Instance.Resolve<TasksViewModel>().TodoTaskCommand.Execute(null);
            MessagingCenter.Subscribe<TasksViewModel, TransitionType>(this, AppSettings.TransitionMessage, (sender, arg) =>
            {
                var transitionType = (TransitionType)arg;
                var transitionNavigationPage = Parent as Control.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;
                    Navigation.PushAsync(new DoneTasksPage());
                    // todo navigate throught command
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<TasksViewModel, TransitionType>(this, AppSettings.TransitionMessage);
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DoneTasksPage());
        }

        /// <summary>
        /// Event OnSearchButtonPressed search task on search pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchText = SeachBarTodoTasks.Text.ToLower();
            var result = ViewModelLocator.Instance.Resolve<TasksViewModel>().PendingTasks.Where(x => x.Content.ToLower().Contains(searchText));
            var enumerable = result as ViewTask[] ?? result.ToArray();
            var any = enumerable.Any();
            ListViewTasks.ItemsSource = any ? (IEnumerable)enumerable : new List<string>() { "Tasks Not Found" };
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
                var serchText = SeachBarTodoTasks.Text.ToLower();
                var searchText = SeachBarTodoTasks.Text.ToLower();
                var result = ViewModelLocator.Instance.Resolve<TasksViewModel>().PendingTasks.Where(x => x.Content.ToLower().Contains(searchText));
                var enumerable = result as ViewTask[] ?? result.ToArray();
                var any = enumerable.Any();
                ListViewTasks.ItemsSource = any ? (IEnumerable)enumerable : new List<string>() { "Tasks Not Found" };

            }   
        }
    }
}