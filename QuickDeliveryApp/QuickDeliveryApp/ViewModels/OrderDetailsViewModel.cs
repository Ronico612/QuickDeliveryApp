using QuickDeliveryApp.Services;
using QuickDeliveryApp.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using OrderDetails = QuickDeliveryApp.Models.OrderDetails;

namespace QuickDeliveryApp.ViewModels
{
    class OrderDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region SelectedOrderDetails
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
        #endregion

        #region RowsHeight
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
        #endregion

        #region IsApproved
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
        #endregion

        #region IsTakenFromShop
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
        #endregion

        #region ApprovedByDeliveryPersonDate
        private DateTime approvedByDeliveryPersonDate;
        public DateTime ApprovedByDeliveryPersonDate
        {
            get
            {
                return this.approvedByDeliveryPersonDate;
            }
            set
            {
                if (this.approvedByDeliveryPersonDate != value)
                {
                    this.approvedByDeliveryPersonDate = value;
                    OnPropertyChanged("ApprovedByDeliveryPersonDate");
                }
            }
        }
        #endregion

        #region IsShowApprovedByDeliveryPersonDate
        private bool isShowApprovedByDeliveryPersonDate;
        public bool IsShowApprovedByDeliveryPersonDate
        {
            get
            {
                return this.isShowApprovedByDeliveryPersonDate;
            }
            set
            {
                if (this.isShowApprovedByDeliveryPersonDate != value)
                {
                    this.isShowApprovedByDeliveryPersonDate = value;
                    OnPropertyChanged("IsShowApprovedByDeliveryPersonDate");
                }
            }
        }
        #endregion

        #region TakenFromShopDate
        private DateTime takenFromShopDate;
        public DateTime TakenFromShopDate
        {
            get
            {
                return this.takenFromShopDate;
            }
            set
            {
                if (this.takenFromShopDate != value)
                {
                    this.takenFromShopDate = value;
                    OnPropertyChanged("TakenFromShopDate");
                }
            }
        }
        #endregion

        #region IsShowTakenFromShopDate
        private bool isShowTakenFromShopDate;
        public bool IsShowTakenFromShopDate
        {
            get
            {
                return this.isShowTakenFromShopDate;
            }
            set
            {
                if (this.isShowTakenFromShopDate != value)
                {
                    this.isShowTakenFromShopDate = value;
                    OnPropertyChanged("IsShowTakenFromShopDate");
                }
            }
        }
        #endregion

        #region BroughtToUserDate
        private DateTime broughtToUserDate;
        public DateTime BroughtToUserDate
        {
            get
            {
                return this.broughtToUserDate;
            }
            set
            {
                if (this.broughtToUserDate != value)
                {
                    this.broughtToUserDate = value;
                    OnPropertyChanged("BroughtToUserDate");
                }
            }
        }
        #endregion

        #region IsShowBroughtToUserDate
        private bool isShowBroughtToUserDate;
        public bool IsShowBroughtToUserDate
        {
            get
            {
                return this.isShowBroughtToUserDate;
            }
            set
            {
                if (this.isShowBroughtToUserDate != value)
                {
                    this.isShowBroughtToUserDate = value;
                    OnPropertyChanged("IsShowBroughtToUserDate");
                }
            }
        }
        #endregion

        #region ShowInDelivery
        private bool showInDelivery;
        public bool ShowInDelivery
        {
            get
            {
                return this.showInDelivery;
            }
            set
            {
                if (this.showInDelivery != value)
                {
                    this.showInDelivery = value;
                    OnPropertyChanged("ShowInDelivery");
                }
            }
        }
        #endregion

        private bool isCalledFromDeliveryPerson;
        private bool isShowOrderStatus; 

        public OrderDetailsViewModel(OrderDetails selectedUserOrderDetails, bool showOrderStatus, bool isCalledFromDeliveryPerson, bool isCalledFromUserCurrentOrders)
        {
            SelectedOrderDetails = selectedUserOrderDetails;
            this.isShowOrderStatus = showOrderStatus;
            this.isCalledFromDeliveryPerson = isCalledFromDeliveryPerson;
            RowsHeight = SelectedOrderDetails.OrderProducts.Count * 65;
            this.IsApproved = this.isCalledFromDeliveryPerson && SelectedOrderDetails.OrderStatusId == 2; // approved
            this.IsTakenFromShop = this.isCalledFromDeliveryPerson && SelectedOrderDetails.OrderStatusId == 3; // taken
            this.InitStatusOrderDetails();
            this.ShowInDelivery = isCalledFromUserCurrentOrders;
        }

        private async void InitStatusOrderDetails()
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            if (this.isShowOrderStatus)
            {
                this.ApprovedByDeliveryPersonDate = await proxy.GetStatusOrderDate(SelectedOrderDetails.OrderId, 2); // approved
                this.IsShowApprovedByDeliveryPersonDate = ApprovedByDeliveryPersonDate != DateTime.MinValue;
            }
            this.TakenFromShopDate = await proxy.GetStatusOrderDate(SelectedOrderDetails.OrderId, 3); // taken
            this.IsShowTakenFromShopDate = TakenFromShopDate != DateTime.MinValue;
            this.BroughtToUserDate = await proxy.GetStatusOrderDate(SelectedOrderDetails.OrderId, 4); // brought
            this.IsShowBroughtToUserDate = BroughtToUserDate != DateTime.MinValue;
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
                this.InitStatusOrderDetails();
                if (app.DeliveryPerson != null)
                {
                    app.DeliveryPerson.UpdateOrderStatus(SelectedOrderDetails.OrderId, 3); // מודיע למשתמש שההזמנה נלקחה מהחנות
                }
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
                if (app.DeliveryPerson != null)
                {
                    app.DeliveryPerson.UpdateOrderStatus(SelectedOrderDetails.OrderId, 4); // מודיע למשתמש שההזמנה נמסרה ללקוח
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שגיאה", "עדכון סטטוס הזמנה נכשל", "בסדר");
        }

        public ICommand ToInDeliveryCommand => new Command(ToInDelivery);
        public async void ToInDelivery()
        {
            Page p = new InDelivery();
            p.Title = "מעקב אחר ההזמנה";
            string originAddress = SelectedOrderDetails.ShopStreet + " " + SelectedOrderDetails.ShopHouseNum + " " + SelectedOrderDetails.ShopCity;
            string destinationAddress = SelectedOrderDetails.OrderStreet + " " + SelectedOrderDetails.OrderHouseNum + " " + SelectedOrderDetails.OrderCity;
            p.BindingContext = new InDeliveryViewModel(SelectedOrderDetails.OrderId, originAddress, destinationAddress, (int)SelectedOrderDetails.OrderStatusId);
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }
    }
}
