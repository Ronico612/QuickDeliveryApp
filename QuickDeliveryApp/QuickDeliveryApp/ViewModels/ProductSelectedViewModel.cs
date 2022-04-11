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

        private bool isGoToShoppingCart;
        public bool IsGoToShoppingCart
        {
            get
            {
                return this.isGoToShoppingCart;
            }
            set
            {
                if (this.isGoToShoppingCart != value)
                {

                    this.isGoToShoppingCart = value;
                    OnPropertyChanged("IsGoToShoppingCart");
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

        private bool errorProductText;
        public bool ErrorProductText
        {
            get
            {
                return this.errorProductText;
            }
            set
            {
                if (this.errorProductText != value)
                {

                    this.errorProductText = value;
                    OnPropertyChanged("ErrorProductText");
                }
            }
        }

        private bool isEnabledButtonAddProduct;
        public bool IsEnabledButtonAddProduct
        {
            get
            {
                return this.isEnabledButtonAddProduct;
            }
            set
            {
                if (this.isEnabledButtonAddProduct != value)
                {

                    this.isEnabledButtonAddProduct = value;
                    OnPropertyChanged("IsEnabledButtonAddProduct");
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
            IsGoToShoppingCart = false;
            IsAddedText = "הוספה לסל הקניות";
            IconSource = "Plus.png";
            ErrorProductText = false;
            IsEnabledButtonAddProduct = true;
        }

        public ICommand AddToShoppingCartCommand => new Command(AddToShoppingCart);
        public async void AddToShoppingCart()
        {
            App app = (App)Application.Current;
            bool isFound = false;
            bool isError = false;
            int countInList;

            if (!(app.ProductsInShoppingCart.Count == 0 || (app.ProductsInShoppingCart.Count > 0 && app.ProductsInShoppingCart.FirstOrDefault().ShopId == this.CurrentProduct.ShopId)))
            {
                await App.Current.MainPage.DisplayAlert("", "לא ניתן להוסיף לסל הקניות מוצרים מחנויות שונות", "בסדר", FlowDirection.RightToLeft);
                IconSource = "";
            }
            else
            {
                foreach (ProductShoppingCart p in app.ProductsInShoppingCart)
                {
                    if (p.ProductId == CurrentProduct.ProductId)
                    {
                        countInList = p.Count;
                        if (!isFound)
                        {
                            if (countInList + 1 <= this.CurrentProduct.CountProductInShop)
                            {
                                p.Count++;
                            }
                            else
                                isError = true;
                            isFound = true;
                        }
                    }
                }

                if (!isFound)
                {
                    if (this.CurrentProduct.CountProductInShop > 0)
                    { 
                        ProductShoppingCart productShoppingCart = new ProductShoppingCart(CurrentProduct);
                        app.ProductsInShoppingCart.Add(productShoppingCart);
                        app.UpdateShoppingCartPage();
                    }
                    else
                    {
                        isError = true;
                    }
                }

                if (isError)
                {
                    ErrorProductText = true;
                    IconSource = "";
                }
                else
                {
                    IsAddedText = "הפריט נוסף לסל הקניות";
                    IconSource = "Done.png";
                }

            }
            IsEnabledButtonAddProduct = false;
            IsGoToShoppingCart = true;
        }

        public ICommand GoToShoppingCartCommand => new Command(GoToShoppingCart);
        public async void GoToShoppingCart()
        {
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PopToRootAsync();
            TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
            theTabs.CurrentTab(theTabs.shoppingCart);
        }

    }
}
