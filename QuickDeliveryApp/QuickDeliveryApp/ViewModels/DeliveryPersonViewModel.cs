using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Views;

namespace QuickDeliveryApp.ViewModels
{
    class DeliveryPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ChooseOrderCommand => new Command(ChooseOrder);
        public async void ChooseOrder()
        {
            Page p = new ChooseOrder();
            p.Title = "בחר הזמנה למשלוח";
            p.BindingContext = new ChooseOrderViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        public ICommand OrdersInProcessCommand => new Command(OrdersInProcess);
        public async void OrdersInProcess()
        {
            Page p = new OrdersInProcess();
            p.Title = "משלוחים בתהליך";
            p.BindingContext = new OrdersInProcessViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

        public ICommand HistoryDeliverysCommand => new Command(HistoryDelivery);
        public async void HistoryDelivery()
        {
            Page p = new HistoryDelivery();
            p.Title = "הזמנות";
            p.BindingContext = new HistoryDeliveryViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }

    }
}
