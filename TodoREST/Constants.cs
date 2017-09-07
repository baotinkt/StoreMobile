using System;

namespace StoreMobile
{
	public static class Constants
	{
        // URL of REST service
        public static string Url = "http://phugia.hopto.org/StoreMobile/";
		public static string Business = Url + "api/Business/";
        public static string User = Url + "api/User/";
        public static string Customer = Url + "api/Customer/";

        //General
        public static string Login = User + "Login";
        public static string CustomerGet = Customer + "CustomerGet?id={0}";

        //Product
        public static string ProductList = Business + "ProductList";
        public static string ProductGet = Business + "ProductGet?id={0}";
        public static string ProductInsert = Business + "ProductInsert";
        public static string ProductUpdate = Business + "ProductUpdate";
        public static string ProductDelete = Business + "ProductDelete?id={0}";

        //TicketObject
        public static string TicketList = Business + "TicketList";
        public static string TicketObjectGet = Business + "TicketObjectGet?id={0}";
        public static string TicketObjectInsert = Business + "Post";
        public static string TicketObjectUpdate = Business + "Put";
        public static string TicketObjectDelete = Business + "Delete?id={0}";

        //TicketObject
        public static string TicketDetailList = Business + "TicketDetailList?ticketId={0}";
        public static string TicketDetailObjectGet = Business + "TicketDetailGet?id={0}";
        public static string TicketDetailObjectInsert = Business + "TicketDetailInsert";
        public static string TicketDetailObjectUpdate = Business + "TicketDetailUpdate";
        public static string TicketDetailObjectDelete = Business + "TicketDetailDelete?id={0}";


        public static string Image = Url + "images/";
        // Credentials that are hard coded into the REST service
        public static string Username = "Xamarin";
		public static string Password = "Pa$$w0rd";
	}
}
