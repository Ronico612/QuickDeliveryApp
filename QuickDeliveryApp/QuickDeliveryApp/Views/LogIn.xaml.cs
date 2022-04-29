using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuickDeliveryApp.ViewModels;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            this.BindingContext = new LoginViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoginViewModel context = (LoginViewModel)this.BindingContext;
            context.ShowCities = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App theApp = (App)App.Current;
            theApp.goToPaymentAfterLogin = false;
        }

        private void Password_Focused(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;
            entry.IsPassword = true;
        }


    }
}