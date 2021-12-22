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

        private ObservableCollection<Product> filteredProducts;
        public ObservableCollection<Product> FilteredProducts
        {
            get
            {
                return this.filteredProducts;
            }
            set
            {
                if (this.filteredProducts != value)
                {

                    this.filteredProducts = value;
                    OnPropertyChanged("FilteredProducts");
                }
            }
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

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get
            {
                return this.selectedProduct;
            }
            set
            {
                if (this.selectedProduct != value)
                {

                    this.selectedProduct = value;
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public Shop CurrentShop;
        public ShopProductsViewModel(Shop selected)
        {
            CurrentShop = selected;
            InitAgeTypes();   
        }

        private void InitProducts()
        {
            this.FilteredProducts = new ObservableCollection<Product>();
            foreach (Product p in this.CurrentShop.Products)
            {
                if (p.AgeProductTypeId == SelectedAgeType.AgeProductTypeId && p.ProductTypeId == selectedProductType.ProductTypeId)
                {
                    if (this.FilteredProducts.Where(a => a.ProductId == p.ProductId).FirstOrDefault() == null)
                        this.FilteredProducts.Add(p);
                }
            }
        }

        private void InitAgeTypes()
        {
            GetAgeTypes();  
            this.SelectedAgeType = AgeTypes.First();
            ShowProductTypesForSelectedAge();
            InitProducts();
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


        private void ShowProductTypesForSelectedAge()
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
            this.SelectedProductType = ProductTypes.First();
            InitProducts();
        }

        public ICommand ShowProductTypesCommand => new Command(ShowProductTypesForSelectedAge);
        public ICommand ShowProductsCommand => new Command(InitProducts);
        public ICommand ShowProductCommand => new Command(ShowProduct);
        public async void ShowProduct()
        {
            if (SelectedProduct != null)
            {
                Page p = new ProductSelected();
                p.Title = SelectedProduct.ProductName;
                p.BindingContext = new ProductSelectedViewModel(this.SelectedProduct);
                TheMainTabbedPage tabbed = (TheMainTabbedPage)Application.Current.MainPage;
                NavigationPage shops = tabbed.shopsPage;
                await shops.Navigation.PushAsync(p);

            }
        }
    }
}
