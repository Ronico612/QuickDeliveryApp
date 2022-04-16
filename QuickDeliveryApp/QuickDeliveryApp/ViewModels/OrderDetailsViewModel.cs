using QuickDeliveryApp.Services;
using QuickDeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class OrderDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private OrderDetails selectedOrderDetails;
        public OrderDetails SelectedOrderDetails
        {
            get
            {
                return this.selectedOrderDetails;
            }
            set
            {
                if (this.selectedOrderDetails != value)
                {

                    this.selectedOrderDetails = value;
                    OnPropertyChanged("SelectedOrderDetails");
                }
            }
        }

        private int rowsHeight;
        public int RowsHeight
        {
            get
            {
                return this.rowsHeight;
            }
            set
            {
                if (this.rowsHeight != value)
                {

                    this.rowsHeight = value;
                    OnPropertyChanged("RowsHeight");
                }
            }
        }

        private bool isApproved;
        public bool IsApproved
        {
            get
            {
                return this.isApproved;
            }
            set
            {
                if (this.isApproved != value)
                {

                    this.isApproved = value;
                    OnPropertyChanged("IsApproved");
                }
            }
        }

        private bool isTakenFromShop;
        public bool IsTakenFromShop
        {
            get
            {
                return this.isTakenFromShop;
            }
            set
            {
                if (this.isTakenFromShop != value)
                {

                    this.isTakenFromShop = value;
                    OnPropertyChanged("IsTakenFromShop");
                }
            }
        }

        private bool isCalledFromDeliveryPerson;

        public OrderDetailsViewModel(OrderDetails selectedUserOrderDetails, bool isCalledFromDeliveryPerson)
        {
            SelectedOrderDetails = selectedUserOrderDetails;
            this.isCalledFromDeliveryPerson = isCalledFromDeliveryPerson;
            RowsHeight = SelectedOrderDetails.OrderProducts.Count * 50;
            this.IsApproved = this.isCalledFromDeliveryPerson && SelectedOrderDetails.OrderStatusId == 2; // approved
            this.IsTakenFromShop = this.isCalledFromDeliveryPerson && SelectedOrderDetails.OrderStatusId == 3; // taken
        }

        public ICommand TakenFromShopCommand => new Command(TakenFromShop);
        public async void TakenFromShop()
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            App app = (App)Application.Current;
            if (await proxy.UpdateStatusOrder(SelectedOrderDetails.OrderId, app.CurrentUser.UserId, 3)) // taken
            {
                this.IsApproved = false;
                this.IsTakenFromShop = true;
            }
            else
                await App.Current.MainPage.DisplayAlert("שגיאה", "עדכון סטטוס הזמנה נכשל", "בסדר");
        }

        public ICommand BroughtToClientCommand => new Command(BroughtToClient);
        public async void BroughtToClient()
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            App app = (App)Application.Current;
            if (await proxy.UpdateStatusOrder(SelectedOrderDetails.OrderId, app.CurrentUser.UserId, 4)) // brought
            {
                await App.Current.MainPage.DisplayAlert("", "הזמנה טופלה בהצלחה, תודה!", "בסדר");
                await app.MainPage.Navigation.PopAsync();
                this.IsApproved = false;
                this.IsTakenFromShop = false;
            }
            else
                await App.Current.MainPage.DisplayAlert("שגיאה", "עדכון סטטוס הזמנה נכשל", "בסדר");
        }
    }
}
