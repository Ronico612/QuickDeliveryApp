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

        public OrderDetailsViewModel(UserOrderDetails selectedUserOrderDetails)
        {
            SelectedOrderDetails = selectedUserOrderDetails;
            RowsHeight = SelectedOrderDetails.OrderProducts.Count * 50;
        }
    }
}
