using QuickDeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuickDeliveryApp.ViewModels
{
    class OrderDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private UserOrderDetails selectedOrderDetails;
        public UserOrderDetails SelectedOrderDetails
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

        public OrderDetailsViewModel(UserOrderDetails selectedUserOrderDetails)
        {
            selectedOrderDetails = selectedUserOrderDetails;

        }
    }
}
