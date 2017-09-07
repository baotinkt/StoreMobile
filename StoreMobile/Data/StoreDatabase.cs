using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace StoreMobile.Data
{
    public class StoreDatabase
    {
        readonly SQLiteAsyncConnection database;

        public StoreDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            try
            {
                database.Table<Customer>().FirstAsync();
            }
            catch (Exception)
            {
                database.CreateTableAsync<OnlineProduct>().Wait();
                database.CreateTableAsync<TicketObject>().Wait();
                database.CreateTableAsync<TicketDetailObject>().Wait();
                database.CreateTableAsync<Customer>().Wait();
            }
        }

        #region OnlineProduct
        public Task<List<OnlineProduct>> GetProductsAsync()
        {
            return database.Table<OnlineProduct>().ToListAsync();
        }

        public Task<OnlineProduct> GetProductAsync(int id)
        {
            return database.Table<OnlineProduct>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveProductAsync(OnlineProduct item, bool isNew)
        {
            if (isNew) return database.UpdateAsync(item);
            else return database.InsertAsync(item);
        }

        public async Task<bool> SaveProductsAsync(List<OnlineProduct> lst, bool isNew)
        {
            try
            {
                if (isNew)
                {
                    foreach (OnlineProduct item in lst)
                        await database.UpdateAsync(item);
                }
                else
                {
                    foreach (OnlineProduct item in lst)
                        await database.InsertAsync(item);
                }
            }
            catch (Exception) { return false; }
            return true;
        }

        public Task<int> DeleteProductAsync(OnlineProduct item)
        {
            return database.DeleteAsync(item);
        }
        #endregion

        #region Ticket
        public Task<List<TicketObject>> GetTicketsAsync()
        {
            return database.Table<TicketObject>().ToListAsync();
        }

        public Task<TicketObject> GetTicketAsync(int id)
        {
            return database.Table<TicketObject>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTicketAsync(TicketObject item, bool isNew)
        {
            if (isNew) return database.UpdateAsync(item);
            else return database.InsertAsync(item);
        }

        public async Task<bool> SaveTicketsAsync(List<TicketObject> lst, bool isNew)
        {
            try
            {
                if (isNew)
                {
                    foreach (TicketObject item in lst)
                        await database.UpdateAsync(item);
                }
                else
                {
                    foreach (TicketObject item in lst)
                        await database.InsertAsync(item);
                }
            }
            catch (Exception) { return false; }
            return true;
        }

        public Task<int> DeleteTicketAsync(TicketObject item)
        {
            return database.DeleteAsync(item);
        }
        #endregion

        #region TicketDetail
        public Task<List<TicketDetailObject>> GetTicketDetailsAsync()
        {
            return database.Table<TicketDetailObject>().ToListAsync();
        }

        public Task<TicketDetailObject> GetTicketDetailAsync(int id)
        {
            return database.Table<TicketDetailObject>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTicketDetailAsync(TicketDetailObject item, bool isNew)
        {
            if (isNew) return database.UpdateAsync(item);
            else return database.InsertAsync(item);
        }

        public async Task<bool> SaveTicketDetailsAsync(List<TicketDetailObject> lst, bool isNew)
        {
            try
            {
                if (isNew)
                {
                    foreach (TicketDetailObject item in lst)
                        await database.UpdateAsync(item);
                }
                else
                {
                    foreach (TicketDetailObject item in lst)
                        await database.InsertAsync(item);
                }
            }
            catch (Exception) { return false; }
            return true;
        }

        public Task<int> DeleteTicketDetailAsync(TicketDetailObject item)
        {
            return database.DeleteAsync(item);
        }
        #endregion

        #region Customer
        public Task<Customer> GetFirstCustomerAsync()
        {
            return database.Table<Customer>().FirstAsync();
        }

        public Task<List<Customer>> GetCustomersAsync()
        {
            return database.Table<Customer>().ToListAsync();
        }

        public Task<Customer> GetCustomerAsync(int id)
        {
            return database.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCustomerAsync(Customer item, bool isNew)
        {
            if (isNew) return database.InsertAsync(item);
            else return database.UpdateAsync(item);
        }

        public Task<int> DeleteCustomerAsync(Customer item)
        {
            return database.DeleteAsync(item);
        }
        #endregion

    }
}

