using System;
using Xamarin.Forms;

namespace TodoREST
{
	public partial class TodoListPage : ContentPage
	{

		public TodoListPage ()
		{
			InitializeComponent ();
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = await App.TodoManager.GetTasksAsync ();
		}

		void OnAddItemClicked (object sender, EventArgs e)
		{
            var OnlineProduct = new OnlineProduct();
            //var OnlineProduct = new OnlineProduct () {
            //	Id = Guid.NewGuid ().ToString ()
            //};
            var todoPage = new OnlineProductPage (true);
			todoPage.BindingContext = OnlineProduct;
			Navigation.PushAsync (todoPage);
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
            TodoREST.OnlineProduct OnlineProduct = (TodoREST.OnlineProduct)e.SelectedItem;
            OnlineProduct.Init(OnlineProduct);
			var todoPage = new OnlineProductPage ();
            if (OnlineProduct.UpdatedDate == null) todoPage._updatedDate = DateTime.Now;
            else todoPage._updatedDate = OnlineProduct.UpdatedDate.Value;
            //OnlineProduct.ImageUrl =Constants.Image + OnlineProduct.Id + ".jpg";
            todoPage.BindingContext = OnlineProduct;

            Navigation.PushAsync (todoPage);
		}
	}
}
