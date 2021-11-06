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

namespace QuickDeliveryApp.ViewModels
{
    class ShopsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Shop> allShops;
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
            this.SearchTerm = string.Empty;
            InitShops();
        }

        private void InitShops()
        {
            Task task = GetAllShops(); // לקבל את רשימת החנויות 
            //task.Wait(); 
            this.FilteredShops = new ObservableCollection<Shop>(this.allShops.OrderBy(s => s.ShopName));
        }

        private async Task GetAllShops()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.allShops = await quickDeliveryAPIProxy.GetShopsAsync();
        }



        public void OnTextChanged(string search)
        {
            //Filter the list of shops based on the search term
            if (this.allShops == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (Shop s in this.allShops)
                {
                    if (!this.FilteredShops.Contains(s))
                        this.FilteredShops.Add(s);
                }
            }
            else
            {
                foreach (Shop s in this.allShops)
                {
                    string contactString = $"{s.ShopName}|{s.ShopCity}";

                    if (!this.FilteredShops.Contains(s) &&
                        contactString.Contains(search))
                        this.FilteredShops.Add(s);
                    else if (this.FilteredShops.Contains(s) &&
                        !contactString.Contains(search))
                        this.FilteredShops.Remove(s);
                }
            }
            this.FilteredShops = new ObservableCollection<Shop>(this.FilteredShops.OrderBy(s => s.ShopName));
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


    }
}
