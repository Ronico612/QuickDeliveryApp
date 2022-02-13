using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;

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
        public void ShopOrders()
        {
        }
    }
}
