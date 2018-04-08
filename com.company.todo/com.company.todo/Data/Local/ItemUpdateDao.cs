using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.todo.Data.Base;
using com.company.todo.Models;

namespace com.company.todo.Data.Local
{
    public class ItemUpdateDao : BaseDatabase 
    {
        private static ItemUpdateDao _instance;
        public static ItemUpdateDao Instance => _instance ?? (_instance = new ItemUpdateDao());

        public ItemUpdateDao()
        {
            _dbConn.CreateTableAsync<ItemUpdate>().Wait();
        }

        public async Task<IEnumerable<ItemUpdate>> GetItemUpdateAsync()
        {
            try
            {
                return await _dbConn.Table<ItemUpdate>().ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to retrieve data. {0}", ex.Message);
            }
            return Enumerable.Empty<ItemUpdate>();
        }

        public async Task AddItemUpdateAsync(ItemUpdate itemUpdate)
        {
            try
            {
                var result = await _dbConn.InsertAsync(itemUpdate);
                System.Diagnostics.Debug.WriteLine("{0} record(s) added [UpdateAt: {1})", result, itemUpdate.UpdateAt);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add {0}. Error: {1}", itemUpdate.UpdateAt, ex.Message);
            }
        }

        public async Task EditItemUpdateAsync(ItemUpdate itemUpdate)
        {
            try
            {
                await _dbConn.UpdateAsync(itemUpdate);
                System.Diagnostics.Debug.WriteLine("record(s) edited [UpdateAt: {0})", itemUpdate.UpdateAt);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to edit {0}. Error: {1}", itemUpdate.UpdateAt, ex.Message);
            }
        }

        public async Task DeleteItemUpdateAsync(ItemUpdate itemUpdate)
        {
            try
            {
                await _dbConn.DeleteAsync(itemUpdate);
                System.Diagnostics.Debug.WriteLine("record(s) deleted [UpdateAt: {0})", itemUpdate.UpdateAt);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to delete {0}. Error: {1}", itemUpdate.UpdateAt, ex.Message);
            }
        }
    }
}
