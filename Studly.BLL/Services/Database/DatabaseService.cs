using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLite;
using Studly.BLL.Interfaces.Services;
using Studly.Entities.Base;

namespace Studly.BLL.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection database;

        private async Task Init<T>() where T : BaseEntity, new()
        {
            if (database == null)
            {
                database = new SQLiteAsyncConnection("path",SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            }

            await database.CreateTableAsync<T>();
        }

        public async Task<T> GetEntityAsync<T>(int id) where T : BaseEntity, new()
        {
            await Init<T>();
            return await database.Table<T>().Where(data => data.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetEntitiesAsync<T>() where T : BaseEntity, new()
        {
            await Init<T>();
            return await database.Table<T>().ToListAsync();
        }

        public async Task<int> SaveEntityAsync<T>(T entity) where T : BaseEntity, new()
        {
            await Init<T>();

            if (entity.Id != 0)
            {
               return await database.UpdateAsync(entity);
            }

            return await database.InsertAsync(entity);
        }

        public async Task<int> DeleteEntityAsync<T>(BaseEntity entity) where T : BaseEntity, new()
        {
            await Init<T>();
            return await database.DeleteAsync(entity);
        }
    }
}
