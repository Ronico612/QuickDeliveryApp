using QuickDeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditShop : ContentPage
    {
        public AddOrEditShop()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AddOrEditShopViewModel context = (AddOrEditShopViewModel)this.BindingContext;
            context.ShowShopCities = false;
            context.ShowShopStreets = false;
        }

        public void SetImageSourceEvent(ImageSource obj)
        {
            theImage.Source = obj;
        }
    }
}