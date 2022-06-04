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
using OrderDetails = QuickDeliveryApp.Models.OrderDetails;

namespace QuickDeliveryApp.ViewModels
{
    class HistoryOrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region UserOrders
        private ObservableCollection<Models.OrderDetails> userOrders;
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
        #endregion

        #region SelectedUserOrder
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
        #endregion

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
            this.UserOrders.Clear();
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
                p.Title = "פרטי הזמנה";
                p.BindingContext = new  OrderDetailsViewModel(SelectedUserOrder, false, false, false);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(p);
                SelectedUserOrder = null;
            }
        }
    }
}
