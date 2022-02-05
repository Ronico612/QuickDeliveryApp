using QuickDeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class PayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ProductShoppingCart> productsInShoppingCart;
        public ObservableCollection<ProductShoppingCart> ProductsInShoppingCart
        {
            get
            {
                return this.productsInShoppingCart;
            }
            set
            {
                if (this.productsInShoppingCart != value)
                {

                    this.productsInShoppingCart = value;
                    OnPropertyChanged("ProductsInShoppingCart");
                }
            }
        }

        private string addressUser;
        public string AddressUser
        {
            get
            {
                return this.addressUser;
            }
            set
            {
                if (this.addressUser != value)
                {

                    this.addressUser = value;
                    OnPropertyChanged("AddressUser");
                }
            }
        }

        private string userNumCard;
        public string UserNumCard
        {
            get
            {
                return this.userNumCard;
            }
            set
            {
                if (this.userNumCard != value)
                {

                    this.userNumCard = value;
                    OnPropertyChanged("UserNumCard");
                }
            }
        }

        
        public PayViewModel()
        {
            App app = (App)Application.Current;
            ProductsInShoppingCart = app.ProductsInShoppingCart;
            AddressUser = app.CurrentUser.UserAddress;
            UserNumCard = app.CurrentUser.NumCreditCard;
        }
    }
}
