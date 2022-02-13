using QuickDeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuickDeliveryApp.ViewModels
{
    class AddOrEditProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Product product;
        public Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product != value)
                {

                    this.product = value;
                    OnPropertyChanged("Product");
                }
            }
        }

        private bool isAddded;
        public bool IsAdded
        {
            get
            {
                return this.isAddded;
            }
            set
            {
                if (this.isAddded != value)
                {

                    this.isAddded = value;
                    OnPropertyChanged("IsAddded");
                }
            }
        }

        public AddOrEditProductViewModel(Product p)
        {
            if (p == null)  // הוספת מוצר
            {
                isAddded = true;
                this.Product = new Product();
            }


        }
    }
}
