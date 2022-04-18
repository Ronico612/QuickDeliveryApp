using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class ShopOrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<OrderDetails> shopOrders;
        public ObservableCollection<OrderDetails> ShopOrders
        {
            get
            {
                return this.shopOrders;
            }
            set
            {
                if (this.shopOrders != value)
                {

                    this.shopOrders = value;
                    OnPropertyChanged("ShopOrders");
                }
            }
        }

        private OrderDetails selectedShopOrder;
        public OrderDetails SelectedShopOrder
        {
            get
            {
                return this.selectedShopOrder;
            }
            set
            {
                if (this.selectedShopOrder != value)
                {

                    this.selectedShopOrder = value;
                    OnPropertyChanged("SelectedShopOrder");
                }
            }
        }

        public App App { get; set; }

        public ShopOrdersViewModel()
        {
            this.App = (App)Application.Current;
            ShopOrders = new ObservableCollection<OrderDetails>();
            InitOrders();
        }

        private async void InitOrders()
        {
            await GetShopOrders();
        }

        private async Task GetShopOrders()
        {
            Shop currentShop = this.App.AllShops.Where(s => s.ShopManagerId == this.App.CurrentUser.UserId).FirstOrDefault();

            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            List<Order> orders = await quickDeliveryAPIProxy.GetShopOrders(currentShop.ShopId);
            foreach (Order o in orders)
            {
                if (o.StatusOrderId == 4) // brought
                {
                    OrderDetails userOrderDetails = new OrderDetails(o);
                    ShopOrders.Add(userOrderDetails);
                }
            }
             
            this.ShopOrders = new ObservableCollection<OrderDetails>(this.ShopOrders.OrderByDescending(o => o.OrderId));
        }

        public ICommand SelectOrderCommand => new Command(SelectOrder);
        public async void SelectOrder()
        {
            if (SelectedShopOrder != null)
            {
                Page p = new Views.OrderDetails();
                // p.Title = SelectedUserOrder.ShopName;
                p.BindingContext = new OrderDetailsViewModel(this.SelectedShopOrder, false, false, false);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(p);
                SelectedShopOrder = null;
            }
        }
    }
}
