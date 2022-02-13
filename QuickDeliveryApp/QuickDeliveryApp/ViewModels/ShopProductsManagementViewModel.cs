using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;

namespace QuickDeliveryApp.ViewModels
{
    class ShopProductsManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
        public ShopProductsManagementViewModel()
        {
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
