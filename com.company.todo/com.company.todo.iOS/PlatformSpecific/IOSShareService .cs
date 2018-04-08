using System;
using System.Collections.Generic;
using System.Text;
using com.company.todo.iOS.PlatformSpecific;
using com.company.todo.DependencyService;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSShareService))]
namespace com.company.todo.iOS.PlatformSpecific
{
    public class IOSShareService : IShareServices
    {
        public void ShareTodoContent(string content)
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var rootViewController = window.RootViewController;

            var activityViewController = new UIActivityViewController(new NSObject[] {new NSString(content)}, null)
            {
                ExcludedActivityTypes = new[]
                {
                    UIActivityType.AirDrop,
                    UIActivityType.Print,
                    UIActivityType.Message,
                    UIActivityType.AssignToContact,
                    UIActivityType.SaveToCameraRoll,
                    UIActivityType.AddToReadingList,
                    UIActivityType.PostToFlickr,
                    UIActivityType.PostToVimeo,
                    UIActivityType.PostToTencentWeibo,
                    UIActivityType.PostToWeibo
                }
            };

            rootViewController.PresentViewController(activityViewController, true, null);
        }
    }
}
