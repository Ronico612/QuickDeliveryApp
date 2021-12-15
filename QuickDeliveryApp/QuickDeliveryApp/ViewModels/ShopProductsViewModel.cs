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


        private List<AgeProductType> ageTypesList;
        private List<ProductType> productTypesList;
        private ObservableCollection<AgeProductType> ageTypes;
        public ObservableCollection<AgeProductType> AgeTypes
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

        private AgeProductType selectedAgeType;
        public AgeProductType SelectedAgeType
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
            InitAgeTypes();
        }

        private async void InitAgeTypes()
        {
            await GetAgeTypes();  
            this.AgeTypes = new ObservableCollection<AgeProductType>(this.ageTypesList);
            //(p.AllTypesOfPrducts.Count > 0)
            this.selectedAgeType = AgeTypes.First();

        }

        private async Task GetAgeTypes()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.ageTypesList = await quickDeliveryAPIProxy.GetAgeTypesAsync(CurrentShop.ShopId);
        }

        //להוסיף פעולה שמביאה את כל המוצרים של החנות הספיציפית הזאת ואז מזה לסנן 

        private async Task GetProductTypesForSelectedAge()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.productTypesList = await quickDeliveryAPIProxy.GetProductTypesForSelectedAgeAsync(this.selectedAgeType.AgeProductTypeId, CurrentShop.ShopId);
        }

        public ICommand ShowProductTypesCommand => new Command(ShowProductTypes);
        public async void ShowProductTypes()
        {
            await GetProductTypesForSelectedAge();
            this.ProductTypes = new ObservableCollection<ProductType>(this.productTypesList);
            this.selectedProductType = productTypes.First();
        }
    }
}
