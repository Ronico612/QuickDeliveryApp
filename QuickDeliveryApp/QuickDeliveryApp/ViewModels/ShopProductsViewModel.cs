using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using QuickDeliveryApp.Views;
using QuickDeliveryApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QuickDeliveryApp.Services;
using System.Linq;

namespace QuickDeliveryApp.ViewModels
{
    class ShopProductsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private List<ProductType> allProductTypes;
        private ObservableCollection<ProductType> ageTypes;
        public ObservableCollection<ProductType> AgeTypes
        {
            get
            {
                return this.ageTypes;
            }
            set
            {
                if (this.ageTypes != value)
                {

                    this.ageTypes = value;
                    OnPropertyChanged("AgeTypes");
                }
            }
        }

        public ShopProductsViewModel(Shop selected)
        {
            Shop CurrentShop = selected;
            InitProductTypes();
        }

        private async void InitProductTypes()
        {
            await GetAllTypeProducts(); // לקבל את רשימת החנויות 
            this.AgeTypes = new ObservableCollection<ProductType>(this.allProductTypes.Where(p => p.ProductTypeId >= 1 && p.ProductTypeId <= 4).OrderBy(pp => pp.ProductTypeId));
        }

        private async Task GetAllTypeProducts()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.allProductTypes = await quickDeliveryAPIProxy.GetProductTypesAsync();
        }

    }
}
