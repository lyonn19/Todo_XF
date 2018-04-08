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
    /// <summary>
    /// Render todoItem Updates Log
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateLogPage : ContentPage
    {
        public UpdateLogPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<UpdateLogViewModel>();
        }
    }
}