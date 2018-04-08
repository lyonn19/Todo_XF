using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace com.company.todo.DependencyService
{
    /// <summary>
    /// Interface for SqliteConection using in DependencyService
    /// </summary>
    public interface ISqLite
    {
        SQLiteAsyncConnection GetConnection();
    }
}
