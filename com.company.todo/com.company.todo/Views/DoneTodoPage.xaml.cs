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
    /// <summary>
    /// Render doneItems List
    /// </summary>
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModelLocator.Instance.Resolve<DoneViewModel>().DoneCommand.Execute(null);
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

        /// <summary>
        /// Event Handle_ItemTapped in ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var task = ((ListView)sender).SelectedItem as TodoItem;
            if (task == null) return;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().SelectedTodoItem = task;
            ListViewTasks.SelectedItem = null;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().NavigateToDetail.Execute(null);
        }

        /// <summary>
        /// Event Menu ContextAction OnDone : update itemTodo status (true) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDone(object sender, EventArgs e)
        {
            var todo = ((MenuItem)sender).CommandParameter as TodoItem;
            if (todo == null) return;

            todo.Status = false;
            ViewModelLocator.Instance.Resolve<EditTodoViewModel>().SelectedTodoItem = todo;
            ViewModelLocator.Instance.Resolve<EditTodoViewModel>().EditTodoCommand.Execute(null);
            ViewModelLocator.Instance.Resolve<DoneViewModel>().DoneCommand.Execute(null);
        }

        /// <summary>
        /// Event Menu ContextAction OnEdit : NavigateToEditPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnEdit(object sender, EventArgs e)
        {
            var todo = ((MenuItem)sender).CommandParameter as TodoItem;
            if (todo == null) return;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().SelectedTodoItem = todo;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().NavigateToEdit.Execute(null);
        }

        /// <summary>
        /// Event Menu ContextAction OnDelete : delete itemTodo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnDelete(object sender, EventArgs e)
        {
            var todo = ((MenuItem)sender).CommandParameter as TodoItem;
            if (todo == null) return;
            var answ = await DisplayAlert("Alert", "Are you sure?", "Cancel", "Accept");
            if (answ) return;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().SelectedTodoItem = todo;
            ViewModelLocator.Instance.Resolve<DoneViewModel>().DeleteTodoCommand.Execute(null);
        }
    }
}