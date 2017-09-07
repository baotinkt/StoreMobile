using System;
using Xamarin.Forms;

namespace StoreMobile
{
	public partial class ProductListPage : ContentPage
	{

		public ProductListPage()
		{
			InitializeComponent ();
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
            listView.IsPullToRefreshEnabled = true;
			listView.ItemsSource = await App.TodoManager.GetProductListTasksAsync ();
		}

		void OnAddItemClicked (object sender, EventArgs e)
		{
            var OnlineProduct = new OnlineProduct();
            //var OnlineProduct = new OnlineProduct () {
            //	Id = Guid.NewGuid ().ToString ()
            //};
            var todoPage = new ProductPage (true);
			todoPage.BindingContext = OnlineProduct;
			Navigation.PushAsync (todoPage);
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
            StoreMobile.OnlineProduct product = (StoreMobile.OnlineProduct)e.SelectedItem;
            product.Init(product);
			var todoPage = new ProductPage ();
            if (product.UpdatedDate == null) todoPage._updatedDate = DateTime.Now;
            else todoPage._updatedDate = product.UpdatedDate.Value;
            //OnlineProduct.ImageUrl =Constants.Image + OnlineProduct.Id + ".jpg";
            todoPage.BindingContext = product;

            Navigation.PushAsync (todoPage);
		}

        async void OnGetActivated(object sender, EventArgs e)
        {
            var OnlineProduct = (OnlineProduct)BindingContext;
            //string test = await App.TodoManager.GetProductTasksAsync(Convert.ToInt32(OnlineProduct.Id));
        }
    }
}
