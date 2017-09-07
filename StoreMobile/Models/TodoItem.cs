using System;

namespace TodoREST
{
    public class OnlineProduct
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Product { get; set; }
        public string UnitCode { get; set; }
        public Nullable<int> Remain { get ; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> WholeSalePrice { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsBestSale { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public string ImageUrl { get; set; }
        public OnlineProduct()
        {
            Barcode = "";
            Product = "";
            UnitCode = "";
            Remain = 0;
            Price = 0;
            WholeSalePrice = 0;
            IsBestSale = false;
            IsNew = false;
            UpdatedDate = DateTime.Now;
            ImageUrl = "";
        }
        public void Init(OnlineProduct item)
        {
            if (item.Barcode == null) item.Barcode = "";
            if (item.IsBestSale == null) item.IsBestSale = false;
            if (item.IsNew == null) item.IsNew = false;
            if (item.UnitCode == null) item.UnitCode = "";
            if (item.Remain == null) item.Remain = 0;
            if (item.Price == null) item.Price = 0;
            if (item.WholeSalePrice == null) item.WholeSalePrice = 0;
            if (item.UpdatedDate == null) item.UpdatedDate = DateTime.Now;
        }
    }
}
