using QuickDeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using QuickDeliveryApp.Services;
using System.Linq;

namespace QuickDeliveryApp.ViewModels
{
    class AdminShowUsersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                if (this.users != value)
                {
                    this.users = value;
                    OnPropertyChanged("Users");
                }
            }
        }

        public AdminShowUsersViewModel()
        {
            InitUsers();
        }

        public async void InitUsers()
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            List<User> userList = await proxy.GetUsersAsync();
            userList = userList.OrderBy(u => u.UserFname).ThenBy(u => u.UserLname).ThenBy(u => u.UserEmail).ToList();
            this.Users = new ObservableCollection<User>(userList);
        }

    }
}
