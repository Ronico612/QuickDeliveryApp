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
using System.Windows.Input;
using QuickDeliveryApp.Views;

namespace QuickDeliveryApp.ViewModels
{
    class HistoryOrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<OrderDetails> userOrders;
        public ObservableCollection<OrderDetails> UserOrders
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

        private OrderDetails selectedUserOrder;
        public OrderDetails SelectedUserOrder
        {
            get
            {
                return this.selectedUserOrder;
            }
            set
            {
                if (this.selectedUserOrder != value)
                {

                    this.selectedUserOrder = value;
                    OnPropertyChanged("SelectedUserOrder");
                }
            }
        }

        public App App { get; set; }

        public HistoryOrdersViewModel()
        {
            this.App = (App)Application.Current;
            UserOrders = new ObservableCollection<OrderDetails>();
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
                OrderDetails userOrderDetails = new OrderDetails();
                userOrderDetails.TotalPrice = o.TotalPrice;
                userOrderDetails.OrderDate = o.OrderDate.ToString();
                userOrderDetails.OrderId = o.OrderId;

                if (o.OrderProducts.Count > 0)
                {
                    userOrderDetails.ShopName = o.OrderProducts.ToList()[0].Product.Shop.ShopName;
                    userOrderDetails.ShopCity = o.OrderProducts.ToList()[0].Product.Shop.ShopCity;
                    userOrderDetails.OrderProducts = new List<OrderProduct>(o.OrderProducts);
                    userOrderDetails.OrderAddress = o.OrderAddress;
                    userOrderDetails.OrderCity = o.OrderCity;
                    userOrderDetails.User = o.User;
                    UserOrders.Add(userOrderDetails);
                }
            }

            this.UserOrders = new ObservableCollection<OrderDetails>(this.UserOrders.OrderByDescending(o => o.OrderId));
        }

        public ICommand SelectUserOrderCommand => new Command(SelectUserOrder);
        public async void SelectUserOrder()
        {
            if (SelectedUserOrder != null)
            {
                Page p = new Views.OrderDetails();
               // p.Title = SelectedUserOrder.ShopName;
                p.BindingContext = new  OrderDetailsViewModel(this.SelectedUserOrder);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(p);
                SelectedUserOrder = null;
            }
        }

    }

    public class OrderDetails
    {
        public string ShopName { get; set; }
        public string ShopCity { get; set; }
        public decimal? TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public int OrderId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string OrderAddress { get; set; }
        public string OrderCity { get; set; }
        public User User { get; set; }
    }
}
