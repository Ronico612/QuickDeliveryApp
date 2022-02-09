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
        public PersonalArea personalArea;

        public TheMainTabbedPage()
        {
            this.BindingContext = new TheMainTabbedPageViewModel();
            InitializeComponent();

            shopsPage = new Shops();
            shopsPage.Title = "חנויות";
            shopsPage.IconImageSource = "Search.png";
            this.Children.Add(shopsPage);

            shoppingCart = new ShoppingCart();
            shoppingCart.Title = "סל קניות";
            shoppingCart.IconImageSource = "ShoppingBag.png";
            this.Children.Add(shoppingCart);

            login = new Login();
            login.Title = "התחברות";
            login.IconImageSource = "Login.png";
            this.Children.Add(login);

            personalArea = new PersonalArea();
            personalArea.IconImageSource = "Login.png";
            personalArea.Title = "אזור אישי";
        }

        public void AddTab(Xamarin.Forms.Page p)
        {
            if (!this.Children.Contains(p))
                this.Children.Add(p);
        }

        public void RemoveTab(Xamarin.Forms.Page p)
        {
            if (this.Children.Contains(p))
                this.Children.Remove(p);
        }

        public void CurrentTab(Xamarin.Forms.Page p)
        {
            this.CurrentPage = p;
        }

        
    }
}