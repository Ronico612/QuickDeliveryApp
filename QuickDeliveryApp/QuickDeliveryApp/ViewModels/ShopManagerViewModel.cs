using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;
using QuickDeliveryApp.Models;
using System.Linq;

namespace QuickDeliveryApp.ViewModels
{
    class ShopManagerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ShopProductsManagementCommand => new Command(ShopProductsManagement);
        public async void ShopProductsManagement()
        {
            Page p = new ShopProductsManagement();
            p.Title = "ניהול מוצרים";
            p.BindingContext = new ShopProductsManagementViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        public ICommand ShopOrdersCommand => new Command(ShopOrders);
        public async void ShopOrders()
        {
            Page p = new ShopOrders();
            p.Title = "הזמנות";
            p.BindingContext = new ShopOrdersViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        private string shopName;
        public string ShopName
        {
            get { return shopName; }
            set
            {
                shopName = value;
                OnPropertyChanged("ShopName");
            }
        }

        private string shopCity;
        public string ShopCity
        {
            get { return shopCity; }
            set
            {
                shopCity = value;
                OnPropertyChanged("ShopCity");
            }
        }

        public ShopManagerViewModel()
        {
            this.InitShopDetails();
        }

        public void InitShopDetails()
        {
            App app = (App)Application.Current;
            if ((app.AllShops != null) && (app.CurrentUser != null))
            {
                Shop currentShop = app.AllShops.Where(s => s.ShopManagerId == app.CurrentUser.UserId).FirstOrDefault();
                if (currentShop != null)
                {
                    this.ShopName = currentShop.ShopName;
                    this.ShopCity = currentShop.ShopCity;
                }
            }
        }
    }
}
