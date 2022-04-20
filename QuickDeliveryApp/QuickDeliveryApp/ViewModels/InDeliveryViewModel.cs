using QuickDeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
