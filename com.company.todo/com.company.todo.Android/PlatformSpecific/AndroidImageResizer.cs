using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.company.todo.DependencyService;
using com.company.todo.Droid.PlatformSpecific;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidImageResizer))]
namespace com.company.todo.Droid.PlatformSpecific
{
    class AndroidImageResizer : IImageResize
    {
        public AndroidImageResizer()
        {
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

    }
}