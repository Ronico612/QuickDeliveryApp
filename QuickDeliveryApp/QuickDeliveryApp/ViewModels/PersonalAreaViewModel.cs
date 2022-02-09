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

        public ICommand LogoutCommand => new Command(Logout);
        public void Logout()
        {
            App.CurrentUser = null;
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
            theTabs.AddTab(theTabs.login);
            theTabs.CurrentTab(theTabs.login);
            theTabs.RemoveTab(theTabs.personalArea);
        }
    }
}
