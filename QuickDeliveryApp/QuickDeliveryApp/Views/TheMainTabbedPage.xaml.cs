using QuickDeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuickDeliveryApp.Services;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheMainTabbedPage : TabbedPage
    {
        public NavigationPage shopsPage;
        public NavigationPage shopProductsPage;
        public ShoppingCart shoppingCart;

        public TheMainTabbedPage()
        {
            this.BindingContext = new TheMainTabbedPageViewModel();
            InitializeComponent();

            shopProductsPage = new NavigationPage(new ShopProducts());
            //shopProductsPage.Title = "Shops";
            //this.Children.Add(shopProductsPage);

            shopsPage = new NavigationPage(new Shops());
            shopsPage.Title = "Shops";
            this.Children.Add(shopsPage);

            shoppingCart = new ShoppingCart();
            shoppingCart.Title = "Shopping Cart";
            this.Children.Add(shoppingCart);
        }
    }
}