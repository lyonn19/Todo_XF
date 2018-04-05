using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using com.company.todo.DependencyService;
using com.company.todo.iOS.PlatformSpecific;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSSqlite))]
namespace com.company.todo.iOS.PlatformSpecific
{
    /// <summary>
    /// IOS specific implementation to get SQLite conetion.
    /// </summary>
    public class IOSSqlite : ISqLite
    {
        #region ISQLite implementation in iOS

        public SQLiteAsyncConnection GetConnection()
        {
            const string sqliteFilename = "todo.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                #region For New DB
                File.Create(path);
                #endregion
            }

            //var plat = new SQLitePlatformIOS();
            var conn = new SQLiteAsyncConnection(path);

            // Return the database connection 
            return conn;
        }

        #endregion
    }
}
