using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;




namespace TodoREST.Data
{
    public class ProductDatabase : SQLiteConnection
    {
        static object locker = new object();

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "ProductsDB.db3";

#if NETFX_CORE
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
#else

#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
				var path = sqliteFilename;
#else

#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder
#endif
                var path = Path.Combine(libraryPath, sqliteFilename);
#endif

#endif
                return path;
            }
        }

        public ProductDatabase(string path) : base(path)
        {
            // create the tables
            CreateTable<Products>();
        }

        public IEnumerable<Products> GetProducts()
        {
            lock (locker)
            {
                return (from i in Table<Products>() select i).ToList();
            }
        }

        public Products GetProduct(int id)
        {
            lock (locker)
            {
                return Table<Products>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveProduct(Products item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    Update(item);
                    return item.Id;
                }
                else
                {
                    return Insert(item);
                }
            }
        }

        //		public int DeleteProducts(int id) 
        //		{
        //			lock (locker) {
        //				return Delete<Products> (new Products () { Id = id });
        //			}
        //		}
        public int DeleteProduct(Products Products)
        {
            lock (locker)
            {
                return Delete<Products>(Products.Id);
            }
        }
    }
}