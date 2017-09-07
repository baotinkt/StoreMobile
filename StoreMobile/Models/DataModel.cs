using System;

namespace StoreMobile
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

    public class TicketObject
    {
        public int Id { set; get; }
        public string Contract { set; get; }
        public DateTime CreatedDate { set; get; }
        public int IsStatus { set; get; }
        public string TicketStatus { set; get; }

        public string Actions { set; get; }
        public TicketObject() { }
        public void Status() {
            switch (IsStatus)
            {
                case 1:
                    this.TicketStatus = "New";
                    this.Actions = "Delete";
                    break;
                case 2:
                    this.TicketStatus = "Confirmed";
                    this.Actions = "Delete";
                    break;
                case 3:
                    this.TicketStatus = "Deliveried";
                    this.Actions = "Delete | Return";
                    break;
                case 4:
                    this.TicketStatus = "Paid";
                    break;
                case 5:
                    this.TicketStatus = "Finish";
                    break;
            }
        }
        public TicketObject(TicketObject item)
        {
            this.Id = item.Id;
            this.Contract = item.Contract;
            this.CreatedDate = item.CreatedDate;
            this.IsStatus = item.IsStatus;
            switch (item.IsStatus)
            {
                case 1:
                    this.TicketStatus = "New";
                    break;
                case 2:
                    this.TicketStatus = "Confirmed";
                    break;
                case 3:
                    this.TicketStatus = "Deliveried";
                    break;
                case 4:
                    this.TicketStatus = "Paid";
                    break;
                case 5:
                    this.TicketStatus = "Finish";
                    break;
            }
        }
    }

    public class TicketDetailObject
    {
        public int Id { set; get; }
        public int TicketId { set; get; }
        public int ProductId { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public decimal Total { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsWholeSale { get; set; }
        public DateTime ProductUpdated { get; set; }
        public DateTime TicketUpdated { get; set; }
        public DateTime TicketDetailUpdated { get; set; }
    }
}

