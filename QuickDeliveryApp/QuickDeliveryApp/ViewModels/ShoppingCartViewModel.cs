using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;
using QuickDeliveryApp.Views;
using System.Threading.Tasks;

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

        private double totalPrice;
        public double TotalPrice
        {
            get
            {
                return this.totalPrice;
            }
            set
            {
                if (this.totalPrice != value)
                {

                    this.totalPrice = value;
                    OnPropertyChanged("TotalPrice");
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
            TotalPrice = 0;
            foreach (ProductShoppingCart p in ProductsInShoppingCart)
            {
                TotalPrice += Decimal.ToDouble(p.ProductPrice) * p.Count;
            }
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
        public async void RemoveCountProduct(ProductShoppingCart productShoppingCart)
        {
            if (productShoppingCart.Count == 1)
            {
                bool answer = await App.Current.MainPage.DisplayAlert("", "האם ברצונך להוריד את הפריט מסל הקניות?", "כן", "לא", FlowDirection.RightToLeft);
                if (answer)
                {
                    TotalPrice -= Decimal.ToDouble(productShoppingCart.ProductPrice);
                    productShoppingCart.Count = 0;
                    this.ProductsInShoppingCart.Remove(productShoppingCart);
                }
            }
            if (productShoppingCart.Count > 1)
            {
                productShoppingCart.Count--;
                TotalPrice -= Decimal.ToDouble(productShoppingCart.ProductPrice);
                productShoppingCart.ErrorText = "";
            }
        }

        public ICommand AddCountProductCommand => new Command<ProductShoppingCart>(AddCountProduct);
        public void AddCountProduct(ProductShoppingCart productShoppingCart)
        {
            if (productShoppingCart.Count + 1 > productShoppingCart.CountProductInShop)
                productShoppingCart.ErrorText = "הפריט אזל מהמלאי";
            else
            {
                productShoppingCart.Count++;
                TotalPrice += Decimal.ToDouble(productShoppingCart.ProductPrice);

            }
        }

        public ICommand DeleteProductsCommand => new Command<ProductShoppingCart>(DeleteProducts);
        public async void DeleteProducts(ProductShoppingCart productShoppingCart)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("", "האם ברצונך להוריד את הפריט מסל הקניות?", "כן", "לא", FlowDirection.RightToLeft);
            if (answer)
            {
                TotalPrice -= Decimal.ToDouble(productShoppingCart.ProductPrice) * productShoppingCart.Count;
                productShoppingCart.Count = 0;
                this.ProductsInShoppingCart.Remove(productShoppingCart);
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
