using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;


namespace Translator.Data
{
    public class TranslationDb
    {
        readonly SQLiteAsyncConnection database;
        readonly Translation.ID = 1;

        public TranslationDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Translation>().Wait();
        }

        public Task<List<Translation>> GetItemsAsync()
        {
            return database.Table<Translation>().ToListAsync();
        }

        public Task<List<Translation>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Translation>("SELECT * FROM [TranslationDb] WHERE [Done] = 0");
        }

        public Task<Translation> GetItemAsync(int id)
        {
            return database.Table<Translation>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Translation item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Translation item)
        {
            return database.DeleteAsync(item);
        }
    }
}
