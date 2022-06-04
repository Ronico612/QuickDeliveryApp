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

        #region FilteredProducts
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
        #endregion

        #region AgeTypes
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
        #endregion

        #region ProductTypes
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
        #endregion

        #region SelectedAgeType
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
        #endregion

        #region SelectedProductType
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
        #endregion

        #region SelectedProduct
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
        #endregion

        public Shop CurrentShop;

        public ShopProductsViewModel(Shop selected)
        {
            CurrentShop = selected;
            InitAgeTypes();   
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
            InitAgeTypes();
        }
        #endregion

        private void InitAgeTypes()
        {
            GetAgeTypes();
            if (AgeTypes != null && AgeTypes.Count > 0)
            {
                this.SelectedAgeType = AgeTypes.First();
                ShowProductTypesForSelectedAge();
                //InitProducts();
                IsRefreshing = false;
            }
        }

        private void GetAgeTypes()
        {
            this.AgeTypes = new ObservableCollection<AgeProductType>();
            foreach (Product p in this.CurrentShop.Products)
            {
                if (this.AgeTypes.Where(a => a.AgeProductTypeId == p.AgeProductTypeId).FirstOrDefault() == null && p.IsDeleted == false)
                    this.AgeTypes.Add(p.AgeProductType);
            }
        }

        public ICommand ShowProductsCommand => new Command(InitProducts);
        private void InitProducts()
        {
            this.FilteredProducts = new ObservableCollection<Product>();
            foreach (Product p in this.CurrentShop.Products)
            {
                if (p.AgeProductTypeId == SelectedAgeType.AgeProductTypeId && p.ProductTypeId == selectedProductType.ProductTypeId && p.IsDeleted == false)
                {
                    if (this.FilteredProducts.Where(a => a.ProductId == p.ProductId).FirstOrDefault() == null)
                        this.FilteredProducts.Add(p);
                }
            }    
        }

        public ICommand ShowProductTypesCommand => new Command(ShowProductTypesForSelectedAge);
        private void ShowProductTypesForSelectedAge()
        {
            this.ProductTypes = new ObservableCollection<ProductType>();
            foreach (Product p in this.CurrentShop.Products)
            {
                if (p.AgeProductTypeId == SelectedAgeType.AgeProductTypeId && p.IsDeleted == false)
                {
                    if (this.ProductTypes.Where(a => a.ProductTypeId == p.ProductTypeId).FirstOrDefault() == null)
                        this.ProductTypes.Add(p.ProductType);
                }
            }
            this.SelectedProductType = ProductTypes.First();
            InitProducts();
        }

        public ICommand ShowProductCommand => new Command(ShowProduct);
        public async void ShowProduct()
        {
            if (SelectedProduct != null)
            {
                Page p = new ProductSelected();
                //p.Title = SelectedProduct.ProductName;
                p.BindingContext = new ProductSelectedViewModel(this.SelectedProduct);
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                await tabbed.Navigation.PushAsync(p);
                SelectedProduct = null;
            }
        }
    }
}
