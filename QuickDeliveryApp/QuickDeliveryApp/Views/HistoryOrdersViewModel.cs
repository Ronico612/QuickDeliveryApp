using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;

namespace QuickDeliveryApp.Views
{
    public class UserOrderDetails 
    {
        public string ShopName { get; set; }
        public decimal? TotalPrice { get; set; }
        public string OrderDate { get; set; }
    }
    class HistoryOrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<UserOrderDetails> userOrders;
        public ObservableCollection<UserOrderDetails> UserOrders
        {
            get
            {
                return this.userOrders;
            }
            set
            {
                if (this.userOrders != value)
                {

                    this.userOrders = value;
                    OnPropertyChanged("UserOrders");
                }
            }
        }

        public App App { get; set; }

        public HistoryOrdersViewModel()
        {
            this.App = (App)Application.Current;
            UserOrders = new ObservableCollection<UserOrderDetails>();
            InitOrders();
        }

        private async void InitOrders()
        {
            await GetUserOrders(); 
        }

        private async Task GetUserOrders()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            List<Order> orders = await quickDeliveryAPIProxy.GetUserOrders(App.CurrentUser.UserId);
            // לפי ססטוס הזמנה מסויים
            foreach (Order o in orders)
            {
                UserOrderDetails userOrderDetails = new UserOrderDetails();
                userOrderDetails.TotalPrice = o.TotalPrice;
                userOrderDetails.OrderDate = o.OrderDate.ToString();

                if (o.OrderProducts.Count > 0)
                    userOrderDetails.ShopName = o.OrderProducts.ToList()[0].Product.Shop.ShopName;

                UserOrders.Add(userOrderDetails);
            }
        }

    }
}
