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
    public partial class ShopManager : ContentPage
    {
        public ShopManager()
        {
            InitializeComponent();
            this.BindingContext = new ShopManagerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ShopManagerViewModel context = (ShopManagerViewModel)this.BindingContext;
            context.InitShopDetails();
        }
    }
}