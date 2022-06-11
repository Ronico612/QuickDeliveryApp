using QuickDeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using QuickDeliveryApp.Services;
using System.Windows.Input;

namespace QuickDeliveryApp.ViewModels
{
    class ChooseOrderViewModel : INotifyPropertyChanged
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

        public ChooseOrderViewModel()
        {
            InitOrders();
        }

        public async void InitOrders()
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            List<Order> waitingOrders = await proxy.GetWaitingOrdersAsync();
            this.Orders = new ObservableCollection<Order>(waitingOrders);
        }

        public ICommand ToDeliveryCommand => new Command<Order>(ToDelivery);
        public async void ToDelivery(Order orderToDeliver)
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            App app = (App)Application.Current;
            if (await proxy.UpdateStatusOrder(orderToDeliver.OrderId, app.CurrentUser.UserId, 2)) // approved
            {
                if (app.DeliveryPerson != null)
                {
                    app.DeliveryPerson.Orders.Add(orderToDeliver); // מוסיף את ההזמנה לרשימת ההזמנות של השליח
                    app.DeliveryPerson.UpdateOrderStatus(orderToDeliver.OrderId, 2); // מודיע למשתמש שההזמנה אושרה
                }
                await App.Current.MainPage.DisplayAlert("", "איסוף הזמנה עודכן בהצלחה, אנא פנה למשלוחים בתהליך", "בסדר");
                
                await app.MainPage.Navigation.PopAsync();
            }
            else
                await App.Current.MainPage.DisplayAlert("שגיאה", "איסוף הזמנה נכשל", "בסדר");
        }
    }
}
