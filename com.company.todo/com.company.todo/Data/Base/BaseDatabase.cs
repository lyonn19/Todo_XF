using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.DependencyService;
using SQLite;

namespace com.company.todo.Data.Base
{
    public class BaseDatabase
    {
        protected SQLiteAsyncConnection _dbConn;
        /// <summary>
        /// Dependency Service, obtiene la conexión a la base de datos en las disferentes platatormas, cada platafoma implementa
        /// </summary>
        public BaseDatabase()
        {
            _dbConn = Xamarin.Forms.DependencyService.Get<ISqLite>().GetConnection();
        }
    }
}
