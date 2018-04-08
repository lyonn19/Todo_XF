using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.DependencyService;
using SQLite;

namespace com.company.todo.Data.Base
{
    /// <summary>
    /// DataBaseAsync get database conection on each platform through DependencyService
    /// </summary>
    public class BaseDatabase
    {
        protected SQLiteAsyncConnection _dbConn;
        
        public BaseDatabase()
        {
            _dbConn = Xamarin.Forms.DependencyService.Get<ISqLite>().GetConnection();
        }
    }
}
