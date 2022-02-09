using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;

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

        #region Address
        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                ValidateAddress();
                this.UpdateIsFormValid();
                OnPropertyChanged("Address");
            }
        }

        private bool showAddressError;
        public bool ShowAddressError
        {
            get => showAddressError;
            set
            {
                showAddressError = value;
                OnPropertyChanged("ShowAddressError");
            }
        }

        private string addressError;
        public string AddressError
        {
            get => addressError;
            set
            {
                addressError = value;
                OnPropertyChanged("AddressError");
            }
        }

        private void ValidateAddress()
        {
            this.ShowAddressError = string.IsNullOrEmpty(Address);
        }
        #endregion

        #region City
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                ValidateCity();
                this.UpdateIsFormValid();
                OnPropertyChanged("City");
            }
        }

        private bool showCityError;
        public bool ShowCityError
        {
            get => showCityError;
            set
            {
                showCityError = value;
                OnPropertyChanged("ShowCityError");
            }
        }

        private string cityError;
        public string CityError
        {
            get => cityError;
            set
            {
                cityError = value;
                OnPropertyChanged("CityError");
            }
        }

        private void ValidateCity()
        {
            this.ShowCityError = string.IsNullOrEmpty(City);
        }
        #endregion

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

        private bool isFormValid;
        public bool IsFormValid
        {
            get
            {
                return this.isFormValid;
            }
            set
            {
                if (this.isFormValid != value)
                {

                    this.isFormValid = value;
                    OnPropertyChanged("IsFormValid");
                }
            }
        }

        private int rowsHeight;
        public int RowsHeight
        {
            get
            {
                return this.rowsHeight;
            }
            set
            {
                if (this.rowsHeight != value)
                {

                    this.rowsHeight = value;
                    OnPropertyChanged("RowsHeight");
                }
            }
        }

        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }

        public App App { get; set; }

        public PayViewModel()
        {
            this.App = (App)Application.Current;
            ProductsInShoppingCart = App.ProductsInShoppingCart;
            RowsHeight = ProductsInShoppingCart.Count * 50;
            this.AddressError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            Address = App.CurrentUser.UserAddress;
            City = App.CurrentUser.UserCity;
            UserNumCard = "************" + App.CurrentUser.NumCreditCard.Substring(App.CurrentUser.NumCreditCard.Length - 4, 4);
        }

        private void UpdateIsFormValid()
        {
            //Check if any validation failed
            if (ShowAddressError || ShowCityError)
                IsFormValid = false;
            else
                IsFormValid = true;
        }

        public ICommand PayCommand => new Command(Pay);
        public async void Pay()
        {
            ServerStatus = "מבצע הזמנה...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

            bool success = true;
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();

            Order order = new Order();
            order.UserId = App.CurrentUser.UserId;
            order.OrderDate = DateTime.Now;
            order.TotalPrice = App.TotalPrice;

            int newOrderID = await proxy.PostNewOrder(order);

            if (newOrderID != 0)
            {
                foreach (ProductShoppingCart p in ProductsInShoppingCart)
                {
                    bool result = await proxy.RemoveProductCount(p.ProductId, p.Count);
                    if (result)
                    {
                        OrderProduct orderProduct = new OrderProduct();
                        orderProduct.OrderId = newOrderID;
                        orderProduct.ProductId = p.ProductId;
                        orderProduct.Quantity = p.Count;
                        orderProduct.Price = p.ProductTotalPrice;

                        result = await proxy.PostNewOrderProduct(orderProduct);
                        if (!result)
                        {
                            success = false;
                            break;
                        }
                    }
                    else
                    {
                        success = false;
                        break;
                    }
                }
            }
            else
                success = false;

            await proxy.StatusOrderOrRemove(success, newOrderID);
            if (success)
            {
                App.ProductsInShoppingCart.Clear();
                App.UpdateShoppingCartPage();
                await App.Current.MainPage.Navigation.PopModalAsync();

                // להעביר לדף אחר ולהגיד שההזמנה התבצעה
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
               // await tabbed.Navigation.PopToRootAsync();
                
                //TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
                //theTabs.SelectShoppingCartTab();
                Page p = new InDelivery();
                p.Title = "מעקב אחר ההזמנה";
                p.BindingContext = new InDeliveryViewModel();
                tabbed.Navigation.InsertPageBefore(p, tabbed.Navigation.NavigationStack[1]);
                await tabbed.Navigation.PopToRootAsync();
                await tabbed.Navigation.PushAsync(p);
            }
            else
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה", "ההזמנה נכשלה, אנא נסו שנית", "בסדר");
            }
        }
    }
}
