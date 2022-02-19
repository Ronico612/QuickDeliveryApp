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

        private void InitProducts()
        {
            App app = (App)Application.Current;
            Shop currentShop = app.AllShops.Where(s => s.ShopManagerId == app.CurrentUser.UserId).FirstOrDefault();
            //this.ShopProducts = currentShop.Products.
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
