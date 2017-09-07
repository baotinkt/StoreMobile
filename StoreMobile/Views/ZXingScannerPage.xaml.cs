using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace StoreMobile.Views
{
    public partial class ZXingScannerPage : ContentPage
    {
        //public ZXingScannerPage()
        //{
        //    InitializeComponent();
        //}

        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        public ZXingScannerPage(ZXing.Mobile.MobileBarcodeScanningOptions options) : base ()
        {
            var scan = new Button
            {
                Text = "Stop Scanning!"
            };
            scan.Clicked += delegate {
                zxing.IsScanning = !zxing.IsScanning;
                scan.Text = zxing.IsScanning? "Stop Scanning!": "Start Scanning!";
            };

            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };


            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    // Show an alert
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    //store result

                    // Navigate away
                    await Navigation.PopAsync();
                });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            overlay.Children.Add(scan);


            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            // The root page of your application
            Content = grid;
            zxing.Options = options;
            //zxing.IsVisible = true;
            zxing.IsScanning = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }

        void OnScan(object sender, EventArgs e)
        {
            zxing.IsScanning = true;
        }
    }
}

