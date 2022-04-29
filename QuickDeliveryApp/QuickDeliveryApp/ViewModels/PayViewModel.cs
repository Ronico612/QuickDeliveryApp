using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;
using System.Linq;

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

        private List<string> allCities;

        private ObservableCollection<string> filteredCities;
        public ObservableCollection<string> FilteredCities
        {
            get
            {
                return this.filteredCities;
            }
            set
            {
                if (this.filteredCities != value)
                {

                    this.filteredCities = value;
                    OnPropertyChanged("FilteredCities");
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
            if (Address == null)
                this.ShowAddressError = true;
            else
                this.ShowAddressError = string.IsNullOrEmpty(Address.Trim());
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
                OnCityChanged(value);
                ValidateCity();
                this.UpdateIsFormValid();
                OnPropertyChanged("City");
            }
        }

        private string selectedCityItem;
        public string SelectedCityItem
        {
            get => selectedCityItem;
            set
            {
                selectedCityItem = value;
                OnPropertyChanged("SelectedCityItem");
            }
        }

        public ICommand SelectedCity => new Command<string>(OnSelectedCity);
        public void OnSelectedCity(string city)
        {
            if (city != null)
            {
                this.ShowCities = false;
                this.City = city;
            }
        }

        private bool showCities;
        public bool ShowCities
        {
            get => showCities;
            set
            {
                showCities = value;
                OnPropertyChanged("ShowCities");
            }
        }

        public void OnCityChanged(string search)
        {
            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }
            //Filter the list of cities based on the search term
            if (this.allCities == null)
                return;

            if (String.IsNullOrWhiteSpace(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                foreach (string city in this.allCities)
                {
                    if (!this.FilteredCities.Contains(city) && city.Contains(search))
                        this.FilteredCities.Add(city);
                    else if (this.FilteredCities.Contains(city) && !city.Contains(search))
                        this.FilteredCities.Remove(city);
                }
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
            this.ShowCityError = string.IsNullOrEmpty(this.City);
            if (!this.ShowCityError)
            {
                string city = this.allCities.Where(c => c == this.City).FirstOrDefault();
                if (string.IsNullOrEmpty(city))
                {
                    this.ShowCityError = true;
                    this.CityError = ERROR_MESSAGES.BAD_CITY;
                }
            }
            else
                this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            allCities = App.Cities;
            this.FilteredCities = new ObservableCollection<string>();
            ProductsInShoppingCart = App.ProductsInShoppingCart;
            RowsHeight = ProductsInShoppingCart.Count * 65;
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
            order.OrderAddress = Address;
            order.OrderCity = City;

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

                //update all shops with products data
                ServerStatus = "מעדכן נתונים...";
                App app = (App)Application.Current;
                await app.GetAllShops();
                
                // להעביר לדף אחר ולהגיד שההזמנה התבצעה
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PopToRootAsync();
                TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
                theTabs.CurrentTab(theTabs.shopsPage);

                Page p = new InDelivery();
                p.Title = "מעקב אחר ההזמנה";
                p.BindingContext = new InDeliveryViewModel(newOrderID, 1); //waiting
                await tabbed.Navigation.PushAsync(p);

                await App.Current.MainPage.Navigation.PopModalAsync();   
            }
            else
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה", "ההזמנה נכשלה, אנא נסו שנית", "בסדר");
            }
        }
    }
}
