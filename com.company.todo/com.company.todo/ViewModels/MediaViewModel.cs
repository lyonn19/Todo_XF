using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using com.company.todo.DependencyService;
using com.company.todo.ViewModels.Base;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace com.company.todo.ViewModels
{
    /// <summary>
    /// Media Manager use MediaPlugin for camera and gallery access
    /// </summary>
    internal class MediaViewModel : ViewModelBase
    {
        #region Fields
        private ICommand _cameraCommand, _galleryCommand = null;
        #endregion

        #region Properties
        private ImageSource _previewImage;
        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                _previewImage = value;
                //Todo use MessageCenter
                ViewModelLocator.Instance.Resolve<AddTodoViewModel>().ImagenSource = value;
                OnPropertyChanged();
            }
        }

        private byte[] _pImage;
        public byte[] PImage
        {
            get { return _pImage; }
            set
            {
                _pImage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Execute camera picture
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteCameraCommand()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var file =
                    await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });

                if (file == null)
                    return;

                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    PImage = memoryStream.ToArray();
                }

                var resizer = Xamarin.Forms.DependencyService.Get<IImageResize>();

                PImage = resizer.ResizeImage(PImage, 1080, 1080);

                PreviewImage = ImageSource.FromStream(() => new MemoryStream(PImage));

            }
            finally
            {
                IsBusy = false;
            }

        }
        /// <summary>
        /// Execute pick from gallery
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteGalleryCommand()
        {
            var file =
                await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                });


            if (file == null)
                return;

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                PImage = memoryStream.ToArray();
            }

            var resizer = Xamarin.Forms.DependencyService.Get<IImageResize>();

            PImage = resizer.ResizeImage(PImage, 1080, 1080);

            PreviewImage = ImageSource.FromStream(() => new MemoryStream(PImage));

        }
        #endregion

        #region Commands
        public ICommand CameraCommand
        {
            get
            {
                return _cameraCommand ??
                       new Command(async () => await ExecuteCameraCommand(), () => CanExecuteCameraCommand());
            }
        }
        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return false;
            }
            return true;
        }
        
        public ICommand GalleryCommand
        {
            get
            {
                return _galleryCommand ??
                       new Command(async () => await ExecuteGalleryCommand(), () => CanExecuteGalleryCommand());
            }
        }
        public bool CanExecuteGalleryCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Application.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.",
                    "OK");
                return false;
            }
            return true;
        }
        #endregion
    }
}

