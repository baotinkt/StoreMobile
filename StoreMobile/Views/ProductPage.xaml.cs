using System;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;
using StoreMobile.Models;
using StoreMobile.Views;

namespace StoreMobile
{
	public partial class ProductPage : ContentPage
	{
		bool isNewItem;
        public DateTime _updatedDate;

		public ProductPage(bool isNew = false)
		{
			InitializeComponent();
			isNewItem = isNew;
            this.BindingContextChanged += OnBindingContextChanged;
		}

        private void OnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            updatedDate1.Date = _updatedDate.Date;
            updatedDate2.Time = _updatedDate.TimeOfDay;
            ((OnlineProduct)BindingContext).UpdatedDate = _updatedDate;
        }


        private void OnDateChanged(object sender, EventArgs e)
        {
            if (updatedDate1 == null) return;
            //_updatedDate = updatedDate1.Date.Add(updatedDate2.Time);
            updatedDate.Text = updatedDate1.Date.Add(updatedDate2.Time).ToString();
            if (BindingContext != null) ((OnlineProduct)BindingContext).UpdatedDate = Convert.ToDateTime(updatedDate.Text); 
        }

        private void OnTimeChanged(object sender, EventArgs e)
        {
            if (((System.ComponentModel.PropertyChangedEventArgs)e).PropertyName == TimePicker.TimeProperty.PropertyName)
            {
                if (updatedDate1 == null) return;
                //_updatedDate = updatedDate1.Date.Add(updatedDate2.Time);
                //updatedDate.Text = _updatedDate.ToString();
                updatedDate.Text = updatedDate1.Date.Add(updatedDate2.Time).ToString();
                if (BindingContext != null) ((OnlineProduct)BindingContext).UpdatedDate = Convert.ToDateTime(updatedDate.Text);
            }
        }
        async void OnGetActivated(object sender, EventArgs e)
        {
            var OnlineProduct = (OnlineProduct)BindingContext;
            string test = await App.TodoManager.GetProductTasksAsync(Convert.ToInt32(OnlineProduct.Id));
            //await Navigation.PopAsync();
        }

        async void OnSaveActivated (object sender, EventArgs e)
		{
			var OnlineProduct = (OnlineProduct)BindingContext;
            OnlineProduct.Remain = Convert.ToInt32(remain.Text);
            OnlineProduct.Price = Convert.ToInt32(price.Text);
            OnlineProduct.WholeSalePrice = Convert.ToInt32(wholeSalePrice.Text);
            await App.TodoManager.SaveProductTaskAsync (OnlineProduct, isNewItem);
            //var fileStream = new FileStream(String.Format(@"/mnt/sdcard/Download/{0}", fileName), FileMode.Open, FileAccess.Read);
            //var streamContent = new StreamContent(fileStream);

            await Navigation.PopAsync ();
		}

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var OnlineProduct = (OnlineProduct)BindingContext;
			await App.TodoManager.DeleteProductTaskAsync (OnlineProduct);
			await Navigation.PopAsync ();
		}

		void OnCancelActivated (object sender, EventArgs e)
		{
			Navigation.PopAsync ();
		}

		void OnSpeakActivated (object sender, EventArgs e)
		{
            var OnlineProduct = (OnlineProduct)BindingContext;
            App.Speech.Speak (OnlineProduct.Product);
		}

        void OnPicture(object sender, EventArgs e)
        {
            //var picturePage = new Camera();
            //picturePage.BindingContext = new CameraViewModel();
            //VideoPlayerViewModel video = new VideoPlayerViewModel();
            //picturePage.BindingContext = new VideoPlayerViewModel();

            //Navigation.PushAsync(picturePage);
        }
    }
}
