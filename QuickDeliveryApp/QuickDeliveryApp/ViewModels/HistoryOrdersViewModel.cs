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

        public async void InitOrders()
        {
            await GetUserOrders();
        }

        private async Task GetUserOrders()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            List<Order> orders = await quickDeliveryAPIProxy.GetUserOrders(App.CurrentUser.UserId);
            foreach (Order o in orders)
            {
                if (o.StatusOrderId == 4) // brought
                {
                    OrderDetails userOrderDetails = new OrderDetails(o);
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
                p.BindingContext = new  OrderDetailsViewModel(this.SelectedUserOrder, false, false, false);
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
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
        public decimal? TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public int OrderId { get; set; }
        public int? OrderStatusId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string OrderAddress { get; set; }
        public string OrderCity { get; set; }
        public User User { get; set; }

        public OrderDetails(Order o)
        {
            this.TotalPrice = o.TotalPrice;
            this.OrderDate = o.OrderDate.ToString();
            this.OrderId = o.OrderId;
            this.OrderStatusId = o.StatusOrderId;

            if (o.OrderProducts.Count > 0)
            {
                this.ShopName = o.OrderProducts.ToList()[0].Product.Shop.ShopName;
                this.ShopCity = o.OrderProducts.ToList()[0].Product.Shop.ShopCity;
                this.ShopAddress = o.OrderProducts.ToList()[0].Product.Shop.ShopAdress;
                this.ShopPhone = o.OrderProducts.ToList()[0].Product.Shop.ShopPhone;
                this.OrderProducts = new List<OrderProduct>(o.OrderProducts);
                this.OrderAddress = o.OrderAddress;
                this.OrderCity = o.OrderCity;
                this.User = o.User;
            }
        }
    }
}
