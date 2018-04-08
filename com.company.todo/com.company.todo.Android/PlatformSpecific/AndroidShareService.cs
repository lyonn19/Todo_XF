using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.company.todo.DependencyService;
using com.company.todo.Droid.PlatformSpecific;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidShareService))]
namespace com.company.todo.Droid.PlatformSpecific
{
    public class AndroidShareService : IShareServices
    {
        public void ShareTodoContent(string content)
        {
            var context = Forms.Context;
            Activity activity = context as Activity;

            Intent share = new Intent(Intent.ActionSend);
            share.SetType("text/plain");
            share.AddFlags(ActivityFlags.ClearWhenTaskReset);
            share.PutExtra(Intent.ExtraSubject, "TODO 4 Today");
            share.PutExtra(Intent.ExtraText, content);

            activity?.StartActivity(Intent.CreateChooser(share, "Share TODO!"));
        }
    }
}