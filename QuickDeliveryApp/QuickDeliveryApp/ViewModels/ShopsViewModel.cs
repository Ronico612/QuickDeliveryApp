using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Models;
using QuickDeliveryApp.Views;
using System.Collections.Generic;

namespace QuickDeliveryApp.ViewModels
{
    class ShopsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Shop> filteredShops;
        public ObservableCollection<Shop> FilteredShops
        {
            get
            {
                return this.filteredShops;
            }
            set
            {
                if (this.filteredShops != value)
                {
                    this.filteredShops = value;
                    OnPropertyChanged("FilteredShops");
                }
            }
        }

        private Shop selectedShop;
        public Shop SelectedShop
        {
            get
            {
                return this.selectedShop;
            }
            set
            {
                if (this.selectedShop != value)
                {
                    this.selectedShop = value;
                    OnPropertyChanged("SelectedShop");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                if (this.searchTerm != value)
                {
                    this.searchTerm = value;
                    OnTextChanged(value);
                    OnPropertyChanged("SearchTerm");
                }
            }
        }

        public ShopsViewModel()
        {
            this.FilteredShops = new ObservableCollection<Shop>();
            this.SearchTerm = string.Empty;
        }

        public void InitShops()
        {
            App app = (App)Application.Current;

            //this.FilteredShops = new ObservableCollection<Shop>(app.AllShops.Where(s => s.IsDeleted == false).OrderBy(s => s.ShopName));
            OnTextChanged(SearchTerm);
            IsRefreshing = false;
        }

        public void OnTextChanged(string search)
        {
            App app = (App)Application.Current;

            //Filter the list of shops based on the search term
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (Shop s in app.AllShops)
                {
                    if (!this.FilteredShops.Contains(s))
                        this.FilteredShops.Add(s);
                }
            }
            else
            {
                foreach (Shop s in app.AllShops)
                {
                    string contactString = $"{s.ShopName.ToLower()}|{s.ShopCity.ToLower()}";

                    if (!this.FilteredShops.Contains(s) && contactString.Contains(search.ToLower()))
                        this.FilteredShops.Add(s);
                    else if (this.FilteredShops.Contains(s) && !contactString.Contains(search.ToLower()))
                        this.FilteredShops.Remove(s);
                }
            }
            this.FilteredShops = new ObservableCollection<Shop>(this.FilteredShops.Where(s => s.IsDeleted == false).OrderBy(s => s.ShopName));
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
            InitShops(); 
        }
        #endregion

        public ICommand ShowShopProductsCommand => new Command(ShowShopProducts);
        public async void ShowShopProducts()
        {
            if (SelectedShop != null)
            {
                Page shopProductsPage = new ShopProducts();
                shopProductsPage.Title = SelectedShop.ShopName;
                shopProductsPage.BindingContext = new ShopProductsViewModel(this.SelectedShop);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(shopProductsPage);
            }
        }
    }
}
