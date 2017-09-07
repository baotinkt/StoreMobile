using System;
using System.Collections.Generic;
using TodoREST.Data;

namespace TodoREST.Droid.Data
{
    public class ProductRepository
    {
        ProductDatabase db = null;
        protected static ProductRepository me;
        static ProductRepository()
        {
            me = new ProductRepository();
        }
        protected ProductRepository()
        {
            db = new ProductDatabase(ProductDatabase.DatabaseFilePath);
        }

        public static Products GetProduct(int id)
        {
            return me.db.GetProduct(id);
        }

        public static IEnumerable<Products> GetProducts()
        {
            return me.db.GetProducts();
        }

        public static int SaveProduct(Products item)
        {
            return me.db.SaveProduct(item);
        }

        public static int DeleteProduct(Products item)
        {
            return me.db.DeleteProduct(item);
        }
    }
}

