using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;
using QuickDeliveryApp.Views;

namespace QuickDeliveryApp.ViewModels
{
    class ShoppingCartViewModel : INotifyPropertyChanged
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

        private ProductShoppingCart selectedProduct;
        public ProductShoppingCart SelectedProduct
        {
            get
            {
                return this.selectedProduct;
            }
            set
            {
                if (this.selectedProduct != value)
                {

                    this.selectedProduct = value;
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public ShoppingCartViewModel()
        {
            IsRefreshing = false;
            App app = (App)Application.Current;
            ProductsInShoppingCart = app.ProductsInShoppingCart;

        }
      
        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            IsRefreshing = false;
        }
        #endregion


        public ICommand RemoveCountProductCommand => new Command<ProductShoppingCart>(RemoveCountProduct);
        public void RemoveCountProduct(ProductShoppingCart productShoppingCart)
        {
            if (productShoppingCart.Count > 0)
                productShoppingCart.Count--;
        }

        public ICommand AddCountProductCommand => new Command<ProductShoppingCart>(AddCountProduct);
        public void AddCountProduct(ProductShoppingCart productShoppingCart)
        {
            if (productShoppingCart.Count + 1 > productShoppingCart.CountProductInShop)
                ErrorText = "אין פריט זה במלאי";
            else
            {
                productShoppingCart.Count++;
            }
        }

        public ICommand ShowSelectedProductCommand => new Command(ShowSelectedProduct);
        public async void ShowSelectedProduct()
        {
            if (SelectedProduct != null)
            {

                Page p = new ProductSelected();
                p.Title = SelectedProduct.ProductName;
                p.BindingContext = new ProductSelectedViewModel(this.SelectedProduct);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(p);

            }
        }
    }
}
