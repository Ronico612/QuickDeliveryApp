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
    class OrdersInProcessViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Orders
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
        #endregion

        public OrdersInProcessViewModel()
        {
            InitOrders();
        }

        public async void InitOrders()
        {
            App app = (App)Application.Current;
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            List<Order> approvedOrTakenOrders = await proxy.GetApprovedOrTakenOrders(app.CurrentUser.UserId);
            this.Orders = new ObservableCollection<Order>(approvedOrTakenOrders);
        }

        public ICommand ToOrderDetailsCommand => new Command<Order>(ToOrderDetails);
        public async void ToOrderDetails(Order orderToDeliver)
        {
            Page p = new Views.OrderDetails();
            p.Title = "פרטי הזמנה";
            OrderDetails orderDetails = new OrderDetails(orderToDeliver);
            p.BindingContext = new OrderDetailsViewModel(orderDetails, true, true, false);
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }
    }
}
