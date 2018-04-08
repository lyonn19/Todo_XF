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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTodoPage : ContentPage
    {
        public DetailTodoPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<DetailTodoViewModel>();
        }

        private void Share(object sender, EventArgs e)
        {
            var share = ViewModelLocator.Instance.Resolve<DetailTodoViewModel>().SelectedTodoItem;
            Xamarin.Forms.DependencyService.Get<IShareServices>().ShareTodoContent(share.Content);
        }
    }
}