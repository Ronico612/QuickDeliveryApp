using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using Xamarin.Forms;
using QuickDeliveryApp.Views;

namespace QuickDeliveryApp.ViewModels
{
    class ProductSelectedViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Product currentProduct;
        public Product CurrentProduct
        {
            get
            {
                return this.currentProduct;
            }
            set
            {
                if (this.currentProduct != value)
                {

                    this.currentProduct = value;
                    OnPropertyChanged("CurrentProduct");
                }
            }
        }
        public ProductSelectedViewModel(Product selectedProduct)
        {
            CurrentProduct = selectedProduct;
        }


    }
}
