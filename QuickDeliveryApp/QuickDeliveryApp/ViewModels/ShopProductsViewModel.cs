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
            GetAgeTypes();  
            //(p.AllTypesOfPrducts.Count > 0)
            this.SelectedAgeType = AgeTypes.First();
            GetProductTypesForSelectedAge();

        }

        private void GetAgeTypes()
        {
            this.AgeTypes = new ObservableCollection<AgeProductType>();
            foreach(Product p in this.CurrentShop.Products)
            {
                if (this.AgeTypes.Where(a => a.AgeProductTypeId == p.AgeProductTypeId).FirstOrDefault() == null)
                    this.AgeTypes.Add(p.AgeProductType);
            }
        }

        //להוסיף פעולה שמביאה את כל המוצרים של החנות הספיציפית הזאת ואז מזה לסנן 

        private void GetProductTypesForSelectedAge()
        {
            this.ProductTypes = new ObservableCollection<ProductType>();
            foreach (Product p in this.CurrentShop.Products)
            {
                if (p.AgeProductTypeId == SelectedAgeType.AgeProductTypeId)
                {
                    if (this.ProductTypes.Where(a => a.ProductTypeId == p.ProductTypeId).FirstOrDefault() == null)
                        this.ProductTypes.Add(p.ProductType);
                }
                
            }
            
        }

        public ICommand ShowProductTypesCommand => new Command(ShowProductTypes);
        public void ShowProductTypes()
        {
            GetProductTypesForSelectedAge();
            this.SelectedProductType = ProductTypes.First();
        }
    }
}
