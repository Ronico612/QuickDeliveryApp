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

        private ObservableCollection<ProductShoppingCart> productsInShoppingCart;
        public ObservableCollection<ProductShoppingCart> ProductsInShoppingCart
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
                    OnPropertyChanged("ProductsInShoppingCart");
                }
            }
        }

        private bool isProductsInList;
        public bool IsProductsInList
        {
            get
            {
                return this.isProductsInList;
            }
            set
            {
                if (this.isProductsInList != value)
                {

                    this.isProductsInList = value;
                    OnPropertyChanged("IsProductsInList");
                }
            }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get
            {
                return this.totalPrice;
            }
            set
            {
                if (this.totalPrice != value)
                {

                    this.totalPrice = value;
                    OnPropertyChanged("TotalPrice");
                }
            }
        }

        public App()
        {
            InitializeComponent();
            ProductsInShoppingCart = new ObservableCollection<ProductShoppingCart>();
            MainPage = new NavigationPage(new TheMainTabbedPage());
        }

        public void UpdateShoppingCartPage()
        {
            this.IsProductsInList = this.ProductsInShoppingCart.Count > 0;
            this.TotalPrice = 0;
            foreach (ProductShoppingCart p in ProductsInShoppingCart)
            {
                TotalPrice += p.ProductTotalPrice;
            }
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
