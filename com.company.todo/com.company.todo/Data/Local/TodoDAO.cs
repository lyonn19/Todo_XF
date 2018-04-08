using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Data.Base;
using com.company.todo.Models;
using Xamarin.Forms.Xaml;

namespace com.company.todo.Data.Local
{
    public class TodoDao : BaseDatabase
    {
        private static TodoDao _instance;
        public static TodoDao Instance => _instance ?? (_instance = new TodoDao());

        public TodoDao()
        {
            _dbConn.CreateTableAsync<TodoItem>().Wait();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoAsync()
        {
            try
            {
                return await _dbConn.QueryAsync<TodoItem>("Select * from [TodoItem] Where Status = ?", false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to retrieve data. {0}", ex.Message);
            }
            return Enumerable.Empty<TodoItem>();
        }

        public async Task<IEnumerable<TodoItem>> GetDoneAsync()
        {
            try
            {
                return await _dbConn.QueryAsync<TodoItem>("Select * from [TodoItem] Where Status = ?", true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to retrieve data. {0}", ex.Message);
            }
            return Enumerable.Empty<TodoItem>();
        }

        public async Task AddTodoAsync(TodoItem todoItem)
        {
            try
            {
                var result = await _dbConn.InsertAsync(todoItem);
                System.Diagnostics.Debug.WriteLine("{0} record(s) added [Content: {1})", result, todoItem.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add {0}. Error: {1}", todoItem.Content, ex.Message);
            }
        }

        public async Task EditTodoAsync(TodoItem todoItem)
        {
            try
            {
                // register update 
                await _dbConn.UpdateAsync(todoItem);
                
                // register update details
                await ItemUpdateDao.Instance.AddItemUpdateAsync(new ItemUpdate()
                {
                    Id = todoItem.Id,
                    UpdatedAt = DateTime.Now
                });
                
                System.Diagnostics.Debug.WriteLine("record(s) edited [Content: {0})",  todoItem.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to edit {0}. Error: {1}", todoItem.Content, ex.Message);
            }
        }

        public async Task DeleteTodoAsync(TodoItem todoItem)
        {
            try
            {
                await _dbConn.DeleteAsync(todoItem);
                System.Diagnostics.Debug.WriteLine("record(s) deleted [Content: {0})", todoItem.Content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to delete {0}. Error: {1}", todoItem.Content, ex.Message);
            }
        }

    }
}
