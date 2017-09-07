using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StoreMobile.Views
{
    public partial class ScanBarcodePage : ContentPage
    {
        public ScanBarcodePage()
        {
            InitializeComponent();
        }

        async void OnScan(object sender, EventArgs e)
        {
            //var scanPage = new ZXingScannerPage();


            // Navigate to our scanner page
            //await Navigation.PushAsync(scanPage);

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
                txtBarcode.Text = result.Text;
            else
                txtBarcode.Text = "Can't scan!";
        }

    }
}
