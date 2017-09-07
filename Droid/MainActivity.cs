using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using XLabs.Platform.Mvvm;
using XLabs.Forms;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Services.Email;
using XLabs.Serialization;
using XLabs.Forms.Services;

namespace StoreMobile.Droid
{
	[Activity (Label = "StoreMobile.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            if (!Resolver.IsSet)
            {
                this.SetIoc();
            }
            //else
            //{
            //    var app = Resolver.Resolve<IXFormsApp>() as IXFormsApp<XFormsApplicationDroid>;
            //    if (app != null) app.AppContext = (XFormsAppDroid)this;
            //}

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.Forms.Forms.ViewInitialized += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };

            global::Xamarin.Forms.Forms.Init (this, bundle);

			App.Speech = new Speech ();
			LoadApplication (new App ());
		}

        /// <summary>
        /// Sets the IoC.
        /// </summary>
        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            var app = new XFormsAppDroid();

            //app.Init(this);

            var documents = app.AppDataDirectory;
            //var pathToDatabase = Path.Combine(documents, "xforms.db");

            resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice)
                .Register<IDisplay>(t => t.Resolve<IDevice>().Display)
                .Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()))
                .Register<IEmailService, EmailService>()
                .Register<IMediaPicker, MediaPicker>()
                .Register<ITextToSpeechService, TextToSpeechService>()
                .Register<IDependencyContainer>(resolverContainer)
                .Register<IXFormsApp>(app)
                .Register<ISecureStorage>(t => new KeyVaultStorage(t.Resolve<IDevice>().Id.ToCharArray()));


            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}

