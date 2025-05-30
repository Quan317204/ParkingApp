using ParkingApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _db;
        public DatabaseService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<VehicleEntry>().Wait();
            _db.CreateTableAsync<User>().Wait();
        }

        public Task<List<VehicleEntry>> GetEntriesAsync() =>
            _db.Table<VehicleEntry>().ToListAsync();

        public Task<int> SaveVehicleEntryAsync(VehicleEntry entry)
        {
            if (entry.Id != 0)
                return _db.UpdateAsync(entry);
            return _db.InsertAsync(entry);
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            return await _db.Table<User>()
                            .Where(u => u.Username == username && u.Password == password)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user) =>
            _db.InsertAsync(user);
    }
}