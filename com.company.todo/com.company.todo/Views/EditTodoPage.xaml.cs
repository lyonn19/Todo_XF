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
    /// Render edit itemTodo
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTodoPage : ContentPage
    {
        public EditTodoPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<EditTodoViewModel>();
        }
        /// <summary>
        /// Event Btn Clicked NavigateBack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }
    }
}