using QuickDeliveryApp.Services;
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
    public partial class ShoppingCart : ContentPage
    {
        public ShoppingCart()
        {
            this.BindingContext = new ShoppingCartViewModel();
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            lbl.Text = await quickDeliveryAPIProxy.GetTestAsync();
        }
    }
}