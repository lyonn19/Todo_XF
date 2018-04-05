using System;
using System.Collections.Generic;
using System.IO;
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
using SQLite;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(AndroidSqlite))]
namespace com.company.todo.Droid.PlatformSpecific
{
    /// <summary>
    /// ANDROID specific implementation to get SQLite conetion.
    /// </summary>
    public class AndroidSqlite : ISqLite
    {
        public AndroidSqlite()
        {
        }

        #region ISQLite implementation in Android

        public SQLiteAsyncConnection GetConnection()
        {
            const string sqliteFilename = "todo.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                #region For New DB
                File.Create(path);
                #endregion
            }
            //var plat = new SQLitePlatformAndroid();
            var conn = new SQLiteAsyncConnection(path);
            // Return the database connection 
            return conn;

        }
        #endregion
    }
}