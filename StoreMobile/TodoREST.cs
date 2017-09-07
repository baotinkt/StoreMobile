using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
//using System.Collections.Generic;
using StoreMobile.Views;
//using ZXing;
using StoreMobile.Data;
using StoreMobile.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StoreMobile
{
	public class App : Application
	{
		public static OnlineProductManager TodoManager { get; private set; }
        static StoreDatabase database;

        public static ITextToSpeech Speech { get; set; }

        public App ()
		{            

            //var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            //options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
            //    ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
            //};

            //MainPage = new NavigationPage(new ZXingScannerPage(options));

            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");

            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                // determine the correct, supported .NET culture
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                Resx.AppResources.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            TodoManager = new OnlineProductManager(new RestService());
            MainPage = new NavigationPage(new LoginPage());

            //var tabs = new TabbedPage();
            //tabs.Children.Add(new FirstPage { Title = "C#", Icon = "csharp.png" });
            //tabs.Children.Add(new FirstPageXaml { Title = "Xaml", Icon = "xaml.png" });

            //MainPage = tabs;
        }

        public static StoreDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new StoreDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Store.db3"));
                }
                return database;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

