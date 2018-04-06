using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Data.Base;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Data.Local
{
    public class TaskDao : BaseDatabase
    {
        private static TaskDao _instance;
        public static TaskDao Instance => _instance ?? (_instance = new TaskDao());

        public TaskDao()
        {
            _dbConn.CreateTableAsync<Models.Task>().Wait();
        }

        public async Task<IEnumerable<Models.Task>> GetTasksAsync()
        {
            try
            {
                return await _dbConn.Table<Models.Task>().ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to retrieve data. {0}", ex.Message);
            }
            return Enumerable.Empty<Models.Task>();
        }

        public async Task AddTaskAsync(Models.Task task)
        {
            try
            {
                var result = await _dbConn.InsertAsync(task);
                System.Diagnostics.Debug.WriteLine("{0} record(s) added [Contet: {1})", result, task.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add {0}. Error: {1}", task.Content, ex.Message);
            }
        }

        public async Task EditTaskAsync(Models.Task task)
        {
            try
            {
                var result = await _dbConn.UpdateAsync(task);
                System.Diagnostics.Debug.WriteLine("{0} record(s) added [Contet: {1})", result, task.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add {0}. Error: {1}", task.Content, ex.Message);
            }
        }

        public async Task DeleteTaskAsync(Models.Task task)
        {
            try
            {
                var result = await _dbConn.DeleteAsync(task);
                System.Diagnostics.Debug.WriteLine("{0} record(s) added [Contet: {1})", result, task.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add {0}. Error: {1}", task.Content, ex.Message);
            }
        }

    }
}
