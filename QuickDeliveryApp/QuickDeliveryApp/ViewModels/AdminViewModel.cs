using QuickDeliveryApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class AdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AdminShopsManagementCommand => new Command(AdminShopsManagement);
        public async void AdminShopsManagement()
        {
            Page p = new AdminShopsManagement();
            p.Title = "ניהול חנויות ומנהלים";
            p.BindingContext = new AdminShopsManagementViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        public ICommand AdminDeliveryPManagementCommand => new Command(AdminDeliveryPManagement);
        public async void AdminDeliveryPManagement()
        {
            Page p = new AdminDeliveryPManagement();
            p.Title = "ניהול שליחים";
            p.BindingContext = new AdminDeliveryPManagementViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        public ICommand AdminShowUsersCommand => new Command(AdminShowUsers);
        public async void AdminShowUsers()
        {
            Page p = new AdminShowUsers();
            p.Title = "משתמשי האפליקציה";
            p.BindingContext = new AdminShowUsersViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }
    }
}
