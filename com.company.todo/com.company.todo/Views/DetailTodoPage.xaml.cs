using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.DependencyService;
using com.company.todo.ViewModels;
using com.company.todo.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Views
{
    /// <summary>
    /// Render todoItem Details
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTodoPage : ContentPage
    {
        public DetailTodoPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<DetailTodoViewModel>();
        }

        /// <summary>
        /// Event for share todoItem contents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Share(object sender, EventArgs e)
        {
            var share = ViewModelLocator.Instance.Resolve<DetailTodoViewModel>().SelectedTodoItem;
            Xamarin.Forms.DependencyService.Get<IShareServices>().ShareTodoContent(share.Content);
        }
    }
}