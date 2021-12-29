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
        public Shops shopsPage;
        public ShoppingCart shoppingCart;
        public Login login;

        public TheMainTabbedPage()
        {
            this.BindingContext = new TheMainTabbedPageViewModel();
            InitializeComponent();

            login = new Login();
            login.Title = "Login";
            login.IconImageSource = "Login.png";
            this.Children.Add(login);

            shopsPage = new Shops();
            shopsPage.Title = "Shops";
            shopsPage.IconImageSource = "Search.png";
            this.Children.Add(shopsPage);

            shoppingCart = new ShoppingCart();
            shoppingCart.Title = "Shopping Cart";
            shoppingCart.IconImageSource = "ShoppingCart.png";
            this.Children.Add(shoppingCart);
        }
    }
}