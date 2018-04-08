using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Services.Navigation;
using com.company.todo.ViewModels.Base;
using com.company.todo.Views;
using Xamarin.Forms;

namespace com.company.todo
{
    /// <summary>
    /// Main App
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TodoPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
