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

        private bool isAddedToShoppingCart;
        public bool IsAddedToShoppingCart
        {
            get
            {
                return this.isAddedToShoppingCart;
            }
            set
            {
                if (this.isAddedToShoppingCart != value)
                {

                    this.isAddedToShoppingCart = value;
                    OnPropertyChanged("IsAddedToShoppingCart");
                }
            }
        }

        private string isAddedText;
        public string IsAddedText
        {
            get
            {
                return this.isAddedText;
            }
            set
            {
                if (this.isAddedText != value)
                {

                    this.isAddedText = value;
                    OnPropertyChanged("IsAddedText");
                }
            }
        }

        private string iconSource;
        public string IconSource
        {
            get
            {
                return this.iconSource;
            }
            set
            {
                if (this.iconSource != value)
                {

                    this.iconSource = value;
                    OnPropertyChanged("IconSource");
                }
            }
        }
        

        public ProductSelectedViewModel(Product selectedProduct)
        {
            CurrentProduct = selectedProduct;
            IsAddedToShoppingCart = false;
            IsAddedText = "הוספה לסל הקניות";
            IconSource = "Plus.png";

        }

        public ICommand AddToShoppingCartCommand => new Command(AddToShoppingCart);
        public void AddToShoppingCart()
        {
            App app = (App)Application.Current;
            bool isFound = false;

            foreach (ProductShoppingCart p in app.ProductsInShoppingCart)
            {
                if (p.ProductId == CurrentProduct.ProductId && !isFound)
                {
                    p.Count++;
                    isFound = true;
                }      
            }

            if (!isFound)
            {
               ProductShoppingCart productShoppingCart = new ProductShoppingCart(CurrentProduct);
               app.ProductsInShoppingCart.Add(productShoppingCart);
            }

            IsAddedToShoppingCart = true;
            IsAddedText = "הפריט נוסף לסל הקניות";
            IconSource = "Done.png";
        }

        //***
        public ICommand GoToShoppingCartCommand => new Command(GoToShoppingCart);
        public async void GoToShoppingCart()
        {
            Page p = new ShoppingCart();
            p.BindingContext = new ShoppingCartViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

    }
}
