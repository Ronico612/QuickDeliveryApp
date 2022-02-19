using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuickDeliveryApp.Views;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.ViewModels;
using System.Threading;

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


        private User currentUser;
        public User CurrentUser
        {
            get
            {
                return this.currentUser;
            }
            set
            {
                if (this.currentUser != value)
                {

                    this.currentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
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

        public List<Shop> AllShops { get; private set; }
        


        public App()
        {
            InitializeComponent();
            ProductsInShoppingCart = new ObservableCollection<ProductShoppingCart>();
            
            ServerStatusViewModel vm = new ServerStatusViewModel();
            vm.ServerStatus = "טוען נתונים....";
            Thread.Sleep(100);
            MainPage = new ServerStatus(vm);
        }

       public string ServerStatus { get; set; }

        public void UpdateShoppingCartPage()
        {
            this.AllShops = new List<Shop>();
            this.IsProductsInList = this.ProductsInShoppingCart.Count > 0;
            this.TotalPrice = 0;
            foreach (ProductShoppingCart p in ProductsInShoppingCart)
            {
                TotalPrice += p.ProductTotalPrice;
            }
        }

        protected async override void OnStart()
        {
            await GetAllShops();
            MainPage = new NavigationPage(new TheMainTabbedPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async Task GetAllShops()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.AllShops = await quickDeliveryAPIProxy.GetShopsAsync();
        }

    }
}
