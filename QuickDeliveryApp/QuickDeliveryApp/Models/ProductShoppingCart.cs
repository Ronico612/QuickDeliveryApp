using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace QuickDeliveryApp.Models
{
    public class ProductShoppingCart : Product, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int count;
        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                if (this.count != value)
                {

                    this.count = value;
                    this.ProductTotalPrice = Count * this.ProductPrice;
                    App app = (App)Application.Current;
                    app.UpdateShoppingCartPage();
                    OnPropertyChanged("Count");
                }
            }
        }

        private string errorText;
        public string ErrorText
        {
            get
            {
                return this.errorText;
            }
            set
            {
                if (this.errorText != value)
                {

                    this.errorText = value;
                    OnPropertyChanged("ErrorText");
                }
            }
        }

        private decimal productTotalPrice;
        public decimal ProductTotalPrice
        {
            get
            {
                return this.productTotalPrice;
            }
            set
            {
                if (this.productTotalPrice != value)
                {

                    this.productTotalPrice = value;
                    OnPropertyChanged("ProductTotalPrice");
                }
            }
        }

        public ProductShoppingCart(Product p)
        {
            this.ProductId = p.ProductId;
            this.ProductName = p.ProductName;
            this.ShopId = p.ShopId;
            this.ProductTypeId = p.ProductTypeId;
            this.AgeProductTypeId = p.AgeProductTypeId;
            this.CountProductInShop = p.CountProductInShop;
            this.ProductPrice = p.ProductPrice;
            this.AgeProductType = p.AgeProductType;
            this.ProductType = p.ProductType;
            this.Shop = p.Shop;
            this.OrderProducts = p.OrderProducts;

            Count = 1;
            ErrorText = "";
        }
    }
}
