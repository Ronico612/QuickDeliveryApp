using QuickDeliveryApp.DTO;
using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class InDeliveryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region IsShowDescription
        private bool isShowDescription;
        public bool IsShowDescription
        {
            get
            {
                return this.isShowDescription;
            }
            set
            {
                if (this.isShowDescription != value)
                {
                    this.isShowDescription = value;
                    OnPropertyChanged("IsShowDescription");
                }
            }
        }
        #endregion

        #region IsWaiting
        private bool isWaiting;
        public bool IsWaiting
        {
            get
            {
                return this.isWaiting;
            }
            set
            {
                if (this.isWaiting != value)
                {
                    this.isWaiting = value;
                    OnPropertyChanged("IsWaiting");
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

        #region IsBrought
        private bool isBrought;
        public bool IsBrought
        {
            get
            {
                return this.isBrought;
            }
            set
            {
                if (this.isBrought != value)
                {
                    this.isBrought = value;
                    OnPropertyChanged("IsBrought");
                }
            }
        }
        #endregion

        private int currentOrderId;
        private int currentStatusId;
        private string origin;
        private string destination;

        public GooglePlace RouteOrigin { get; private set; }
        public GooglePlace RouteDestination { get; private set; }
        public GoogleDirection RouteDirections { get; private set; }

        public event Action OnUpdateMapEvent;
        public event Action<double, double> OnDeliveryLocation;

        //Connection to hub
        private DeliveryProxy deliveryProxy;

        public InDeliveryViewModel(int orderId, string originAddress, string destinationAddress, int statusId, bool isShowDescription = false)
        {
            this.currentOrderId = orderId;
            this.currentStatusId = statusId;

            this.IsWaiting = statusId == 1;
            this.IsApproved = statusId == 2;
            this.IsTakenFromShop = statusId == 3;
            this.IsBrought = statusId == 4;

            this.origin = originAddress;
            this.destination = destinationAddress;
            this.IsShowDescription = isShowDescription;

            //Open connection to delivery proxy
            this.deliveryProxy = new DeliveryProxy();
            this.deliveryProxy.RegisterToUpdateDeliveryLocation(UpdateDeliveryLocation);
            this.deliveryProxy.RegisterToUpdateOrderStatus(UpdateStatusByServer);
        }

        public async void ConnectToDeliveryProxy()
        {
            string[] orders = { currentOrderId.ToString() };
            await this.deliveryProxy.Connect(orders);
        }

        public async void DisconnectToDeliveryProxy()
        {
            string[] orders = { currentOrderId.ToString() };
            await this.deliveryProxy.Disconnect(orders);
        }

        //This method will be called by delivery proxy when the order status is changing
        public async void UpdateStatusByServer(string orderId, string statusId)
        {
            if ((currentOrderId.ToString() == orderId) && (currentStatusId.ToString() != statusId))
            {
                this.IsWaiting = statusId == "1";
                this.IsApproved = statusId == "2";
                this.IsTakenFromShop = statusId == "3";
                this.IsBrought = statusId == "4";

                if (IsBrought)
                {
                    App app = (App)Application.Current;
                    await App.Current.MainPage.DisplayAlert("ההזמנה נמסרה!", "תודה שקנית אצלנו", "אישור", FlowDirection.RightToLeft);
                    await app.MainPage.Navigation.PopToRootAsync();
                }
            }
        }

        //This method update the delivery guy location
        public void UpdateDeliveryLocation(string latitude, string longitude)
        {
            double lat = double.Parse(latitude), longi = double.Parse(longitude);
            if (OnDeliveryLocation != null)
                OnDeliveryLocation(lat, longi);
        }

        public async void OnGo()
        {
            try
            {
                GoogleMapsApiService service = new GoogleMapsApiService();
                //Find auto complete places first for origin and destination
                GooglePlaceAutoCompleteResult originPlaces = await service.GetPlaces(origin);
                GooglePlaceAutoCompleteResult destPlaces = await service.GetPlaces(destination);
                //Extract the exact first google place for origin and destination
                //Note that here i am taking the first suggestion but it will be better if you will
                //Ask the user to choose which suggestion is better for him
                GooglePlace place1 = await service.GetPlaceDetails(originPlaces.AutoCompletePlaces[0].PlaceId);
                GooglePlace place2 = await service.GetPlaceDetails(destPlaces.AutoCompletePlaces[0].PlaceId);
                //Get directions to move from origin to destination
                GoogleDirection direction = await service.GetDirections($"{place1.Latitude}", $"{place1.Longitude}", $"{place2.Latitude}", $"{place2.Longitude}");
                //Update the properties so the main page class will have access to the information and fire the event to update the map
                RouteOrigin = place1;
                RouteDestination = place2;
                RouteDirections = direction;
                if (OnUpdateMapEvent != null)
                    OnUpdateMapEvent();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
