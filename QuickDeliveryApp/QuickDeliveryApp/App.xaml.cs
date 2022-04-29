using System;
using System.Linq;
using Xamarin.Forms;
using QuickDeliveryApp.Views;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.ViewModels;
using System.Threading;
using QuickDeliveryApp.DTO;

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

        public delegate void OrderStatusEventHandler(Object sender, int orderId, int statusId);
        public event OrderStatusEventHandler OnOrderStatusUpdate;

        public List<string> Cities { get; set; }

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

        public bool goToPaymentAfterLogin;

        public App()
        {
            InitializeComponent();
            Cities = new List<string>();
            ProductsInShoppingCart = new ObservableCollection<ProductShoppingCart>();
            ServerStatusViewModel vm = new ServerStatusViewModel();
            vm.IsShowLogo = true;
            MainPage = new ServerStatus(vm);
            this.goToPaymentAfterLogin = false;
        }

       public string ServerStatus { get; set; }

        public void UpdateShoppingCartPage()
        {
            this.IsProductsInList = this.ProductsInShoppingCart.Count > 0;
            this.TotalPrice = 0;
            foreach (ProductShoppingCart p in ProductsInShoppingCart)
            {
                TotalPrice += p.ProductTotalPrice;
            }
        }

        protected async override void OnStart()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            List<City> cities = await quickDeliveryAPIProxy.GetCitiesAsync();
            this.Cities = cities.OrderBy(c => c.name).Select(c => c.name).ToList();
            await GetAllShops();
            MainPage = new NavigationPage(new TheMainTabbedPage())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black
            };
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

        public void CallOrderStatusUpdate(int orderId, int statusId)
        {
            if (OnOrderStatusUpdate != null)
                OnOrderStatusUpdate(this, orderId, statusId);
        }
    }
}
