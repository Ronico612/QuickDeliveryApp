﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuickDeliveryApp.ViewModels
{
    class InDeliveryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string statusText;
        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public InDeliveryViewModel()
        {

        }


    }
}
