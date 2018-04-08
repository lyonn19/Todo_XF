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
    /// Add New todoItem
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTodoPage : ContentPage
    {
        public AddTodoPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<AddTodoViewModel>();
        }
        /// <summary>
        /// Event BtnClicked display options for camera or gallery imagen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Image From", "Cancel", "Delete", "Camera", "Gallery");

            switch (response)
            {
                case "Camera":
                    ViewModelLocator.Instance.Resolve<MediaViewModel>().CameraCommand.Execute(null);
                    break;
                case "Gallery":
                    ViewModelLocator.Instance.Resolve<MediaViewModel>().GalleryCommand.Execute(null);
                    break;
            }
        }
    }
}