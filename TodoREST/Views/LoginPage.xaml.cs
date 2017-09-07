using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StoreMobile.Data;

namespace StoreMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignInActivated(object sender, EventArgs e)
        {
            //List<Customer> lst = await App.Database.GetCustomersAsync();
            //var cus = (Customer)BindingContext;
            Customer cus = new Customer();
            cus.Username = txtUsername.Text;
            cus.Password = txtPassword.Text;
            int customerId = await App.TodoManager.PostLogin(cus);
            if (customerId > 0)
            {
                var page = new TicketListPage();
                await Navigation.PushAsync(page);
            }
            else
            {
                await Navigation.PopAsync();
                lblMsg.Text = "Username or password is wrong!";
            }
        }
    }
}