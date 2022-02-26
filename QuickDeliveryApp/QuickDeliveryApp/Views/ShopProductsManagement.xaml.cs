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
    public partial class ShopProductsManagement : ContentPage
    {
        public ShopProductsManagement()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App app = (App)Application.Current;
            await app.GetAllShops();
            ShopProductsManagementViewModel context = (ShopProductsManagementViewModel)this.BindingContext;
            context.InitProducts();
        }
    }
}