using QuickDeliveryApp.Models;
using QuickDeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class PersonalAreaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public App App { get; set; } 

        public PersonalAreaViewModel()
        {
            this.App = (App)Application.Current;
        }

        public ICommand UpdateUserDetailsCommand => new Command(UpdateUserDetails);
        public async void UpdateUserDetails()
        {
            Page p = new UserDetails();
            //p.Title = "חשבון";
            p.BindingContext = new UserDetailsViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        public ICommand CurrentOrdersCommand => new Command(CurrentOrders);
        public void CurrentOrders()
        {
        }

        public ICommand HistoryOrdersCommand => new Command(HistoryOrders);
        public async void HistoryOrders()
        {
            Page p = new HistoryOrders();
            p.Title = "ההזמנות שלי";
            p.BindingContext = new HistoryOrdersViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }


        public ICommand LogoutCommand => new Command(Logout);
        public void Logout()
        {
            App.CurrentUser = null;
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
            theTabs.AddTab(theTabs.login);
            theTabs.CurrentTab(theTabs.login);
            theTabs.RemoveTab(theTabs.personalArea);
            if (theTabs.Children.Contains(theTabs.shopManager))
                theTabs.RemoveTab(theTabs.shopManager);
            if (theTabs.Children.Contains(theTabs.admin))
                theTabs.RemoveTab(theTabs.admin);
            if (theTabs.Children.Contains(theTabs.deliveryPerson))
                theTabs.RemoveTab(theTabs.deliveryPerson);
        }
    }
}
