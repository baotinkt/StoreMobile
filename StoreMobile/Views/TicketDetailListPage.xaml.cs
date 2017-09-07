using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace StoreMobile
{
	public partial class TicketDetailListPage : ContentPage
	{
        int ticketId;
        List<TicketDetailObject> lstDetail;
		public TicketDetailListPage(int ticketId)
		{
			InitializeComponent ();
            listView.IsPullToRefreshEnabled = true;
            this.ticketId = ticketId;
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
            if (ticketId > 0)
            {
                lstDetail = await App.TodoManager.GetTicketDetailListTasksAsync(ticketId);
                listView.ItemsSource = lstDetail;
            }
		}

		void OnAddItemClicked (object sender, EventArgs e)
		{
            var todoPage = new TicketDetailAddPage (ticketId);
			todoPage.BindingContext = lstDetail;
			Navigation.PushAsync (todoPage);
		}


        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            StoreMobile.TicketObject ticket = (StoreMobile.TicketObject)e.SelectedItem;
            //ticket.Init(ticket);
            var todoPage = new TicketDetailListPage(ticket.Id);
            todoPage.BindingContext = ticket;

            Navigation.PushAsync(todoPage);
        }
    }
}
