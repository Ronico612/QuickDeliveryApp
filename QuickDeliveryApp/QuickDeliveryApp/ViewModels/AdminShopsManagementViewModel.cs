using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.Views;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class AdminShopsManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Shop> adminShops;
        public ObservableCollection<Shop> AdminShops
        {
            get
            {
                return this.adminShops;
            }
            set
            {
                if (this.adminShops != value)
                {
                    this.adminShops = value;
                    OnPropertyChanged("AdminShops");
                }
            }
        }

        public AdminShopsManagementViewModel()
        {
            InitShops();
        }

        public void InitShops()
        {
            App app = (App)Application.Current;
            List<Shop> shops = app.AllShops;
            this.AdminShops = new ObservableCollection<Shop>(shops.Where(s => s.IsDeleted == false).OrderBy(s => s.ShopName).ThenBy(s => s.ShopCity).ThenBy(s => s.ShopAdress));
        }

        public ICommand DeleteShopCommand => new Command<Shop>(DeleteShop);
        public async void DeleteShop(Shop shopToDelete)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("", "האם ברצונך למחוק את החנות?", "כן", "לא", FlowDirection.RightToLeft);
            if (answer)
            {
                QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
                bool isDeleted = await quickDeliveryAPIProxy.DeleteShopAsync(shopToDelete.ShopId);
                await quickDeliveryAPIProxy.DeleteShopManager((int)shopToDelete.ShopManagerId);
                if (isDeleted)
                {
                    App app = (App)Application.Current;
                    await app.GetAllShops();
                    InitShops();
                }
            }
        }

        public ICommand EditShopCommand => new Command<Shop>(EditShop);
        public async void EditShop(Shop shopToEdit)
        {
            AddOrEditShop p = new AddOrEditShop();
            p.Title = "עדכון חנות קיימת";
            AddOrEditShopViewModel vm = new AddOrEditShopViewModel(shopToEdit);
            vm.SetImageSourceEvent += p.SetImageSourceEvent;
            p.BindingContext = vm;
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }


        public ICommand AddShopCommand => new Command(AddShop);
        public async void AddShop()
        {
            AddOrEditShop p = new AddOrEditShop();
            p.Title = "הוספת חנות חדשה";
            AddOrEditShopViewModel vm = new AddOrEditShopViewModel(null);
            vm.SetImageSourceEvent += p.SetImageSourceEvent;
            p.BindingContext = vm;
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }


    }
}
