using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;
using System.Collections.ObjectModel;
using QuickDeliveryApp.Models;
using System.Linq;
using QuickDeliveryApp.Services;

namespace QuickDeliveryApp.ViewModels
{
    class ShopProductsManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Product> shopProducts;
        public ObservableCollection<Product> ShopProducts
        {
            get
            {
                return this.shopProducts;
            }
            set
            {
                if (this.shopProducts != value)
                {
                    this.shopProducts = value;
                    OnPropertyChanged("ShopProducts");
                }
            }
        }

        public ShopProductsManagementViewModel()
        {
            InitProducts();
        }

        public void InitProducts()
        {
            App app = (App)Application.Current;
            Shop currentShop = app.AllShops.Where(s => s.ShopManagerId == app.CurrentUser.UserId).FirstOrDefault();
            List<Product> products = currentShop.Products.Where(p => p.IsDeleted == false).OrderBy(p => p.AgeProductTypeId).ThenBy(p => p.ProductTypeId).ThenBy(p => p.ProductName).ToList();
            this.ShopProducts = new ObservableCollection<Product>(products);
        }


        public ICommand DeleteProductCommand => new Command<Product>(DeleteProduct);
        public async void DeleteProduct(Product productToDelete)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("", "האם ברצונך למחוק את המוצר מהחנות?", "כן", "לא", FlowDirection.RightToLeft);
            if (answer)
            {
                QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
                bool isDeleted = await quickDeliveryAPIProxy.DeleteProductAsync(productToDelete.ProductId);
                if (isDeleted)
                {
                    App app = (App)Application.Current;
                    await app.GetAllShops();
                    InitProducts();
                }          
            }
        }

        public ICommand EditProductCommand => new Command<Product>(EditProduct);
        public async void EditProduct(Product productToEdit)
        {
            Page p = new AddOrEditProduct();
            p.Title = "עדכון מוצר קיים";
            p.BindingContext = new AddOrEditProductViewModel(productToEdit);
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }


        public ICommand AddProductCommand => new Command(AddProduct);
        public async void AddProduct()
        {
            Page p = new AddOrEditProduct();
            p.Title = "הוספת מוצר חדש";
            p.BindingContext = new AddOrEditProductViewModel(null);
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }
    }
}
