using SQLite;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XF_UsingCamera.Model
{
    public class DBOperations
    {
        readonly SQLiteAsyncConnection db;

        public DBOperations(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Users>().Wait();
        }
        
        public Task<int> SaveUserAsync(Users user)
        {
            if (user.Id != 0)
            {
                // Update an existing User.
                return db.UpdateAsync(user);
            }
            else
            {
                // Save a new User.
                return db.InsertAsync(user);
            }
        }

       
        //Get all Users.
        public Task<List<Users>> GetAllUsersAsync()
        {
            return db.Table<Users>().ToListAsync();
        }       
    }
}