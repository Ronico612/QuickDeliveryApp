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

       
        private ObservableCollection<Product> productsInShoppingCart;
        public ObservableCollection<Product> ProductsInShoppingCart
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


        private Product selectedProduct;
        public Product SelectedProduct
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
            ProductsInShoppingCart = new ObservableCollection<Product>(app.ProductsInShoppingCart);

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
           // InitShops();
        }
        #endregion



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
