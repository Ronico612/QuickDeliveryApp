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
    class AdminDeliveryPManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<User> deliveryPersons;
        public ObservableCollection<User> DeliveryPersons
        {
            get
            {
                return this.deliveryPersons;
            }
            set
            {
                if (this.deliveryPersons != value)
                {
                    this.deliveryPersons = value;
                    OnPropertyChanged("DeliveryPersons");
                }
            }
        }

        public AdminDeliveryPManagementViewModel()
        {
            InitDeliveryPersons();
        }

        public async void InitDeliveryPersons()
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            List<User> deliveryPersons = await proxy.GetDeliveryPersons();
            deliveryPersons = deliveryPersons.OrderBy(u => u.UserFname).ThenBy(u => u.UserLname).ToList();
            this.DeliveryPersons = new ObservableCollection<User>(deliveryPersons);
        }


        public ICommand DeleteDeliveryPersonCommand => new Command<User>(DeleteDeliveryPerson);
        public async void DeleteDeliveryPerson(User delPToDelete)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("", "האם ברצונך למחוק את השליח?", "כן", "לא", FlowDirection.RightToLeft);
            if (answer)
            {
                QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
                bool isDeleted = await quickDeliveryAPIProxy.DeleteDeliveryPerson(delPToDelete.UserId);
                if (isDeleted)
                    InitDeliveryPersons();
                else
                    await App.Current.MainPage.DisplayAlert("שגיאה", "מחיקת שליח נכשלה", "בסדר", FlowDirection.RightToLeft);
            }
        }

        public ICommand AddDeliveryPCommand => new Command(AddDeliveryP);
        public async void AddDeliveryP()
        {
            Page p = new AddDeliveryPerson();
            p.Title = "הוספת שליח חדש";
            p.BindingContext = new AddDeliveryPersonViewModel();
            NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
            await tabbed.Navigation.PushAsync(p);
        }
    }
}
