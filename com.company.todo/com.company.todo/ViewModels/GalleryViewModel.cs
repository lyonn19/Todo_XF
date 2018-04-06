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
    public class GalleryImage : ObservableObject
    {
        public GalleryImage()
        {
            ImageId = Guid.NewGuid();
        }

        public Guid ImageId
        {
            get;
            set;
        }

        public ImageSource Source
        {
            get;
            set;
        }

        public byte[] OrgImage
        {
            get;
            set;
        }

    }



    class GalleryViewModel : ViewModelBase
    {
        ICommand _cameraCommand, _galleryCommand, _previewImageCommand = null;

        public GalleryViewModel()
        {
            Images = new ObservableCollection<GalleryImage>();
            //Attachments = new ObservableCollection<Attachment>();
        }

        public ObservableCollection<GalleryImage> Images { get; set; }

        //public ObservableCollection<Attachment> Attachments { get; set; }

        private ImageSource _previewImage;
        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                //SetProperty(ref _previewImage, value);
                _previewImage = value;
                ViewModelLocator.Instance.Resolve<AddTasksViewModel>().Imagen = value;
                OnPropertyChanged();
            }
        }

        public ICommand CameraCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraCommand(), () => CanExecuteCameraCommand()); }
        }

        public ICommand GalleryCommand
        {
            get { return _galleryCommand ?? new Command(async () => await ExecuteGalleryCommand(), () => CanExecuteGalleryCommand()); }
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

        public bool CanExecuteGalleryCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Application.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return false;
            }
            return true;
        }

        public async Task ExecuteGalleryCommand()
        {
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });


            if (file == null)
                return;

            byte[] imageAsBytes = null;
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            var resizer = Xamarin.Forms.DependencyService.Get<IImageResize>();

            imageAsBytes = resizer.ResizeImage(imageAsBytes, 1080, 1080);
            
            if (Images.Count == 0)
            {
                PreviewImage = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
        }

        public async Task ExecuteCameraCommand()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
              
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });

                if (file == null)
                    return;

                byte[] imageAsBytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    imageAsBytes = memoryStream.ToArray();
                }

                var resizer = Xamarin.Forms.DependencyService.Get<IImageResize>();

                imageAsBytes = resizer.ResizeImage(imageAsBytes, 1080, 1080);

                var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

                if (Images.Count == 0)
                {
                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                }
                Images.Add(new GalleryImage { Source = imageSource, OrgImage = imageAsBytes });
                //Attachments.Add(new Attachment() { ContentType = "image/jpeg", FileData = imageAsBytes, FileName = "imagen" + Images.Count });

            }
            finally
            {
                IsBusy = false;
            }

        }

        public ICommand PreviewImageCommand
        {
            get
            {
                return _previewImageCommand ?? new Command<Guid>((img) => {
                    var image = Images.Single(x => x.ImageId == img).OrgImage;
                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));
                });
            }
        }
    }
}
