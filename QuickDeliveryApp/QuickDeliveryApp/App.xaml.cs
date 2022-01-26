using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuickDeliveryApp.Views;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace QuickDeliveryApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv 
        { 
            get
            {
                return true;
            }
        }

        public User CurrentUser
        {
            get; set;
        }

        // שיודיע על שינויים

        private List<Product> productsInShoppingCart;
        public List<Product> ProductsInShoppingCart
        {
            get
            {
                return this.productsInShoppingCart;
            }
            set
            {
                if (this.productsInShoppingCart != value)
                {

                    this.productsInShoppingCart = value;
                }
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TheMainTabbedPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
