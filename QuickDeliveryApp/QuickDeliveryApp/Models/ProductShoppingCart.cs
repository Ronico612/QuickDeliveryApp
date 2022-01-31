using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
                    OnPropertyChanged("Count");
                }
            }
        }


        public ProductShoppingCart(Product p)
        {
            Count = 1;
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
        }
    }
}
