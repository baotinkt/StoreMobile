using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace StoreMobile
{
	public partial class TicketDetailAddPage : ContentPage
	{
        int ticketId;
		public TicketDetailAddPage(int ticketId)
		{
			InitializeComponent ();
            this.ticketId = ticketId;
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = await App.TodoManager.GetProductListTasksAsync ();
		}

        void OnSaveItemClicked(object sender, EventArgs e)
        {
            var detail = new TicketDetailListPage(ticketId);
            Navigation.PushAsync(detail);
        }


        void OnAddItemClicked (object sender, EventArgs e)
		{
            var detail = new TicketDetailListPage(ticketId);
			Navigation.PushAsync (detail);
		}

        void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
        {
        //          StoreMobile.OnlineProduct product = (StoreMobile.OnlineProduct)e.SelectedItem;
        //          product.Init(product);
        //	var todoPage = new ProductPage ();
        //          if (product.UpdatedDate == null) todoPage._updatedDate = DateTime.Now;
        //          else todoPage._updatedDate = product.UpdatedDate.Value;
        //          //OnlineProduct.ImageUrl =Constants.Image + OnlineProduct.Id + ".jpg";
        //          todoPage.BindingContext = product;

        //          Navigation.PushAsync (todoPage);
        }

        //async void OnGetActivated(object sender, EventArgs e)
        //{
        //    var OnlineProduct = (OnlineProduct)BindingContext;
        //    //string test = await App.TodoManager.GetProductTasksAsync(Convert.ToInt32(OnlineProduct.Id));
        //}

        

        async void OnSaveActivated(object sender, EventArgs e)
        {
            List<TicketDetailObject> items = (List<TicketDetailObject>)BindingContext;
            await App.TodoManager.SaveTicketDetailObjectTaskAsync(items);
            await Navigation.PopAsync();
        }
    }
}
