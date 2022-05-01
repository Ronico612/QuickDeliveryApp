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

        //private string origin;
        //public string Origin
        //{
        //    get => this.origin;
        //    set
        //    {
        //        this.origin = value;
        //        OnPropertyChanged("Origin");
        //    }
        //}

        //private string destination;
        //public string Destination
        //{
        //    get => this.destination;
        //    set
        //    {
        //        this.destination = value;
        //        OnPropertyChanged("Destination");
        //    }
        //}

        public GooglePlace RouteOrigin { get; private set; }
        public GooglePlace RouteDestination { get; private set; }
        public GoogleDirection RouteDirections { get; private set; }

        public event Action OnUpdateMapEvent;

        private int currentOrderId;
        private int currentStatusId;

        public InDeliveryViewModel(int orderId, int statusId)
        {
            this.currentOrderId = orderId;
            this.currentStatusId = statusId;

            App app = (App)Application.Current;
            app.OnOrderStatusUpdate += App_OnOrderStatusUpdate;

            this.IsWaiting = statusId == 1;
            this.IsApproved = statusId == 2;
            this.IsTakenFromShop = statusId == 3;
            this.IsBrought = statusId == 4;
        }

        public ICommand Go => new Command(OnGo);
        public async void OnGo()
        {
            try
            {
                GoogleMapsApiService service = new GoogleMapsApiService();
                //find auto complete places first for origin and destination
                GooglePlaceAutoCompleteResult originPlaces = await service.GetPlaces(Origin);
                GooglePlaceAutoCompleteResult destPlaces = await service.GetPlaces(Destination);
                //extract the exact first google place for origin and destination
                //note that here i am taking the first suggestion but it will be better if you will
                //ask the user to choose which suggestion is better for him
                GooglePlace place1 = await service.GetPlaceDetails(originPlaces.AutoCompletePlaces[0].PlaceId);
                GooglePlace place2 = await service.GetPlaceDetails(destPlaces.AutoCompletePlaces[0].PlaceId);
                //get directions to move from origin to destination
                GoogleDirection direction = await service.GetDirections($"{place1.Latitude}", $"{place1.Longitude}", $"{place2.Latitude}", $"{place2.Longitude}");
                //update the properties so the main page class will have access to the information and fire the event to update the map
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

        private void App_OnOrderStatusUpdate(object sender, int orderId, int statusId)
        {
            if ((currentOrderId == orderId) && (currentStatusId != statusId))
            {
                this.IsWaiting = statusId == 1;
                this.IsApproved = statusId == 2;
                this.IsTakenFromShop = statusId == 3;
                this.IsBrought = statusId == 4;
            }
        }
    }
}
