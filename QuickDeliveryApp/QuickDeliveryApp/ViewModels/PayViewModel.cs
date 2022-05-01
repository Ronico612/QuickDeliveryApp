﻿using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;
using System.Linq;
using QuickDeliveryApp.DTO;

namespace QuickDeliveryApp.ViewModels
{
    class PayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public App App { get; set; }

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

        private List<Street> allStreets;

        private ObservableCollection<string> filteredStreets;
        public ObservableCollection<string> FilteredStreets
        {
            get
            {
                return this.filteredStreets;
            }
            set
            {
                if (this.filteredStreets != value)
                {

                    this.filteredStreets = value;
                    OnPropertyChanged("FilteredStreets");
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

        #region City
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    ValidateCity();
                    OnCityChanged(value);
                    OnPropertyChanged("City");
                }
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
            if (!string.IsNullOrEmpty(city))
            {
                this.City = city;
                this.IsStreetEnabled = true;
                this.ShowCities = false;
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
            this.Street = "";
            this.ShowStreets = false;
            this.FilteredStreets.Clear();
            this.IsStreetEnabled = false;

            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }

            if (this.allCities == null)
                return;

            //Filter the list of cities based on the search term
            if (String.IsNullOrWhiteSpace(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                List<string> cityList = this.allCities.Where(c => c.Contains(search)).OrderBy(c => c).ToList();
                this.FilteredCities = new ObservableCollection<string>(cityList);
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

        #region Street

        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnStreetChanged(value);
                ValidateStreet();
                OnPropertyChanged("Street");
            }
        }

        private bool showStreets;
        public bool ShowStreets
        {
            get => showStreets;
            set
            {
                showStreets = value;
                OnPropertyChanged("ShowStreets");
            }
        }

        private string selectedStreetItem;
        public string SelectedStreetItem
        {
            get => selectedStreetItem;
            set
            {
                selectedStreetItem = value;
                OnPropertyChanged("SelectedStreetItem");
            }
        }

        public ICommand SelectedStreet => new Command<string>(OnSelectedStreet);
        public void OnSelectedStreet(string street)
        {
            if (!string.IsNullOrEmpty(street))
            {
                this.ShowStreets = false;
                this.Street = street;
            }
        }

        private bool showStreetError;
        public bool ShowStreetError
        {
            get => showStreetError;
            set
            {
                showStreetError = value;
                OnPropertyChanged("ShowStreetError");
            }
        }

        private string streetError;
        public string StreetError
        {
            get => streetError;
            set
            {
                streetError = value;
                OnPropertyChanged("StreetError");
            }
        }

        private bool isStreetEnabled;
        public bool IsStreetEnabled
        {
            get => isStreetEnabled;
            set
            {
                isStreetEnabled = value;
                OnPropertyChanged("IsStreetEnabled");
            }
        }

        private void ValidateStreet()
        {
            this.ShowStreetError = string.IsNullOrEmpty(this.Street);
            if (!this.ShowStreetError)
            {
                Street street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
                if (street == null)
                {
                    this.ShowStreetError = true;
                    this.StreetError = ERROR_MESSAGES.BAD_STREET;
                }
            }
            else
                this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        public void OnStreetChanged(string search)
        {
            if (this.Street != this.SelectedStreetItem)
            {
                this.ShowStreets = true;
                this.SelectedStreetItem = null;
            }

            if (this.allStreets == null)
                return;

            //Filter the list of streets based on the search term
            if (String.IsNullOrWhiteSpace(search))
            {
                this.ShowStreets = false;
                this.FilteredStreets.Clear();
            }
            else
            {
                List<Street> streetList = this.allStreets.Where(s => s.street_name.Contains(search) && s.city_name == this.City).OrderBy(s => s.street_name).ToList();
                this.FilteredStreets = new ObservableCollection<string>(streetList.Select(s => s.street_name));
            }
        }
        #endregion

        #region StreetNum
        private bool showStreetNumError;
        public bool ShowStreetNumError
        {
            get => showStreetNumError;
            set
            {
                showStreetNumError = value;
                OnPropertyChanged("ShowStreetNumError");
            }
        }

        private string streetNum;
        public string StreetNum
        {
            get => streetNum;
            set
            {
                streetNum = value;
                ValidateStreetNum();
                OnPropertyChanged("StreetNum");
            }
        }

        private string streetNumError;
        public string StreetNumError
        {
            get => streetNumError;
            set
            {
                streetNumError = value;
                OnPropertyChanged("StreetNumError");
            }
        }

        private void ValidateStreetNum()
        {
            this.ShowStreetNumError = string.IsNullOrEmpty(this.StreetNum);
            int num;
            if (!this.ShowStreetNumError)
            {
                if (this.StreetNum.StartsWith("0") || !int.TryParse(this.StreetNum, out num) || num <= 0)
                {
                    this.ShowStreetNumError = true;
                    this.StreetNumError = ERROR_MESSAGES.BAD_STREET_NUM;
                }
            }
            else
                this.StreetNumError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region UserNumCard
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

        #endregion

        public ICommand PayCommand => new Command(Pay);

        public PayViewModel()
        {
            this.App = (App)Application.Current;

            allCities = App.Cities;
            this.FilteredCities = new ObservableCollection<string>();

            this.allStreets = this.App.Streets;
            this.FilteredStreets = new ObservableCollection<string>();

            ProductsInShoppingCart = App.ProductsInShoppingCart;
            RowsHeight = ProductsInShoppingCart.Count * 65;

            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.StreetError = ERROR_MESSAGES.BAD_STREET;
            this.StreetNumError = ERROR_MESSAGES.BAD_STREET_NUM;

            this.City = App.CurrentUser.UserCity;
            this.Street = App.CurrentUser.UserAddress;
            //this.StreetNum = App.CurrentUser.StreetNum;
            UserNumCard = "************" + App.CurrentUser.NumCreditCard.Substring(App.CurrentUser.NumCreditCard.Length - 4, 4);
            this.IsStreetEnabled = true;
        }

        private bool IsFormValid()
        {
            this.ValidateCity();
            this.ValidateStreet();
            this.ValidateStreetNum();

            //Check if any validation failed
            if (ShowStreetError || ShowStreetNumError || ShowCityError)
                return false;
            else
                return true;
        }

        public async void Pay()
        {
            if (!IsFormValid())
                return;

            ServerStatus = "מבצע הזמנה...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

            bool success = true;
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();

            Order order = new Order();
            order.UserId = App.CurrentUser.UserId;
            order.OrderDate = DateTime.Now;
            order.TotalPrice = App.TotalPrice;
            order.OrderCity = City;
            order.OrderAddress = Street;
            //order.OrderStreetNum = StreetNum;

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
                string originAddress = ProductsInShoppingCart[0].Shop.ShopAdress + " " + ProductsInShoppingCart[0].Shop.ShopCity;
                string destinationAddress = Street + " " + StreetNum + " " + City;

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
   
                p.BindingContext = new InDeliveryViewModel(newOrderID, originAddress, destinationAddress, 1); //waiting
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
