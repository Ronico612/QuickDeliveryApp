using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class HistoryDeliveryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get
            {
                return this.orders;
            }
            set
            {
                if (this.orders != value)
                {
                    this.orders = value;
                    OnPropertyChanged("Orders");
                }
            }
        }

        public HistoryDeliveryViewModel()
        {
            InitOrders();
        }

        public async void InitOrders()
        {
            App app = (App)Application.Current;
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            List<Order> HistoryDeliveryPersonOrders = await proxy.GetHistoryDeliveryPersonOrders(app.CurrentUser.UserId);
            this.Orders = new ObservableCollection<Order>(HistoryDeliveryPersonOrders);
        }

        public ICommand ToOrderDetailsCommand => new Command<Order>(ToOrderDetails);
        public async void ToOrderDetails(Order orderToDeliver)
        {
            Page p = new Views.OrderDetails();
            OrderDetails orderDetails = new OrderDetails(orderToDeliver);
            p.BindingContext = new OrderDetailsViewModel(orderDetails, true, true, false);
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }
    }
}
