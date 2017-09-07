using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StoreMobile
{
	public partial class TicketListPage : ContentPage
	{

		public TicketListPage()
		{
			InitializeComponent ();
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
            listView.IsPullToRefreshEnabled = true;
            listView.ItemsSource = await App.TodoManager.GetTicketListTasksAsync ();
		}

		void OnAddItemClicked (object sender, EventArgs e)
		{
            var ticket = new TicketObject();
            //var OnlineProduct = new OnlineProduct () {
            //	Id = Guid.NewGuid ().ToString ()
            //};
            var todoPage = new TicketDetailListPage (0);
			todoPage.BindingContext = ticket;
			Navigation.PushAsync (todoPage);
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
            StoreMobile.TicketObject ticket = (StoreMobile.TicketObject)e.SelectedItem;
            //ticket.Init(ticket);
			var todoPage = new TicketDetailListPage(ticket.Id);
            todoPage.BindingContext = ticket;

            Navigation.PushAsync (todoPage);
		}

        async void OnActionActivated(object sender, EventArgs e)
        {
            await App.TodoManager.DeleteTicketObjectTaskAsync((int)((Button)sender).CommandParameter);
        }

    }
}
