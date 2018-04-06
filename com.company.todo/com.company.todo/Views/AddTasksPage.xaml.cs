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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTasksPage : ContentPage
    {
        public AddTasksPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<AddTasksViewModel>();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Image From", "Cancel", "Delete", "Camera", "Gallery");

            switch (response)
            {
                case "Camera":
                    ViewModelLocator.Instance.Resolve<GalleryViewModel>().CameraCommand.Execute(null);
                    break;
                case "Gallery":
                    ViewModelLocator.Instance.Resolve<GalleryViewModel>().GalleryCommand.Execute(null);
                    break;
            }
        }
    }
}