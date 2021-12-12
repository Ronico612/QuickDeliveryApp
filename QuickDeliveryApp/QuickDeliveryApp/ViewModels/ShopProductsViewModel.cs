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
using System.Windows.Input;
using Xamarin.Forms;

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

        private ObservableCollection<ProductType> productTypes;
        public ObservableCollection<ProductType> ProductTypes
        {
            get
            {
                return this.productTypes;
            }
            set
            {
                if (this.productTypes != value)
                {

                    this.productTypes = value;
                    OnPropertyChanged("ProductTypes");
                }
            }
        }

        private ProductType selectedAgeType;
        public ProductType SelectedAgeType
        {
            get
            {
                return this.selectedAgeType;
            }
            set
            {
                if (this.selectedAgeType != value)
                {

                    this.selectedAgeType = value;
                    OnPropertyChanged("SelectedAgeType");
                }
            }
        }

        private ProductType selectedProductType;
        public ProductType SelectedProductType
        {
            get
            {
                return this.selectedProductType;
            }
            set
            {
                if (this.selectedProductType != value)
                {

                    this.selectedProductType = value;
                    OnPropertyChanged("SelectedProductType");
                }
            }
        }

        public Shop CurrentShop;
        public ShopProductsViewModel(Shop selected)
        {
            CurrentShop = selected;
            InitProductTypes();
        }

        private async void InitProductTypes()
        {
            await GetAllTypeProducts(); // לקבל את רשימת סוגי מוצרים 
            this.AgeTypes = new ObservableCollection<ProductType>(this.allProductTypes.Where(p => p.ProductTypeId >= 1 && p.ProductTypeId <= 4 && p.AllTypesOfPrducts.Count > 0).OrderBy(pp => pp.ProductTypeId));
            this.selectedAgeType = AgeTypes.First();
        }

        //להוסיף פעולה שמביאה את כל המוצרים של החנות הספיציפית הזאת ואז מזה לסנן 

        private async Task GetAllTypeProducts()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.allProductTypes = await quickDeliveryAPIProxy.GetProductTypesAsync(CurrentShop.ShopId);
        }

        public ICommand ShowProductTypesCommand => new Command(ShowProductTypes);
        public void ShowProductTypes()
        {
            this.ProductTypes = new ObservableCollection<ProductType>(this.allProductTypes.Where(p => p.ProductTypeId > 4 && p.AllTypesOfPrducts.Count > 0));
            this.selectedProductType = productTypes.First();

            this.ProductTypes = new ObservableCollection<ProductType>(this.allProductTypes.Where(p => p.ProductTypeId == this.selectedAgeType.ProductTypeId && p.AllTypesOfPrducts.Count > 0));
            this.selectedProductType = productTypes.First();
        }
    }
}
