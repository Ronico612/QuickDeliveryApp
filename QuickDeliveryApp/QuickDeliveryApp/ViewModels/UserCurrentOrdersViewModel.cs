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
    class UserCurrentOrdersViewModel : INotifyPropertyChanged
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

        public UserCurrentOrdersViewModel()
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
            foreach (Order o in orders)
            {
                if (o.StatusOrderId == 1 || o.StatusOrderId == 2 || o.StatusOrderId == 3) // waiting, approved, taken
                {
                    OrderDetails userOrderDetails = new OrderDetails(o);
                    UserOrders.Add(userOrderDetails);
                }
            }

            this.UserOrders = new ObservableCollection<OrderDetails>(this.UserOrders);
        }

        public ICommand SelectUserOrderCommand => new Command(SelectUserOrder);
        public async void SelectUserOrder()
        {
            if (SelectedUserOrder != null)
            {
                Page p = new Views.OrderDetails();
                p.Title = "פרטי הזמנה";
                p.BindingContext = new OrderDetailsViewModel(this.SelectedUserOrder, true, false, true);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(p);
                SelectedUserOrder = null;
            }
        }
    }
}
