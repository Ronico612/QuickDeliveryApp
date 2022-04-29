using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class UserDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> cities;
        public List<string> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                OnPropertyChanged("Cities");
            }
        }


        #region Phone
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        private bool showPhoneError;
        public bool ShowPhoneError
        {
            get => showPhoneError;
            set
            {
                showPhoneError = value;
                OnPropertyChanged("ShowPhoneError");
            }
        }

        private string phoneError;
        public string PhoneError
        {
            get => phoneError;
            set
            {
                phoneError = value;
                OnPropertyChanged("PhoneError");
            }
        }

        private void ValidatePhone()
        {
            if (Phone == null)
                this.ShowPhoneError = true;
            else
                this.ShowPhoneError = string.IsNullOrEmpty(Phone.Trim());
            if (!this.ShowPhoneError)
            {
                if (!Regex.IsMatch(this.Phone, @"^[0-9]*$"))
                {
                    this.ShowPhoneError = true;
                    this.PhoneError = ERROR_MESSAGES.BAD_NUMBER;
                }
                else if (!Regex.IsMatch(this.Phone, @"^\+?(972|0)(\-)?0?(([23489]{1}\d{7})|[5]{1}\d{8})$"))
                {
                    this.ShowPhoneError = true;
                    this.PhoneError = ERROR_MESSAGES.BAD_PHONE;
                }
            }
            else
                this.PhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Address
        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private bool showAddressError;
        public bool ShowAddressError
        {
            get => showAddressError;
            set
            {
                showAddressError = value;
                OnPropertyChanged("ShowAddressError");
            }
        }

        private string addressError;
        public string AddressError
        {
            get => addressError;
            set
            {
                addressError = value;
                OnPropertyChanged("AddressError");
            }
        }

        private void ValidateAddress()
        {
            if (Address == null)
                this.ShowAddressError = true;
            else
                this.ShowAddressError = string.IsNullOrEmpty(Address.Trim());
        }
        #endregion

        #region City
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        private bool showCityError;
        public bool ShowCityError
        {
            get => showCityError;
            set
            {
                showCityError = value;
                OnPropertyChanged("ShowCityError");
            }
        }

        private string cityError;
        public string CityError
        {
            get => cityError;
            set
            {
                cityError = value;
                OnPropertyChanged("CityError");
            }
        }

        private void ValidateCity()
        {
            if (City == null)
                this.ShowCityError = true;
            else
                this.ShowCityError = string.IsNullOrEmpty(City.Trim());
        }
        #endregion

        #region NumCreditCard
        private string numCreditCard;
        public string NumCreditCard
        {
            get { return numCreditCard; }
            set
            {
                numCreditCard = value;
                OnPropertyChanged("NumCreditCard");
            }
        }

        private bool showNumCreditCardError;
        public bool ShowNumCreditCardError
        {
            get => showNumCreditCardError;
            set
            {
                showNumCreditCardError = value;
                OnPropertyChanged("ShowNumCreditCardError");
            }
        }

        private string numCreditCardError;
        public string NumCreditCardError
        {
            get => numCreditCardError;
            set
            {
                numCreditCardError = value;
                OnPropertyChanged("NumCreditCardError");
            }
        }

        private void ValidateNumCreditCard()
        {
            if (NumCreditCard == null)
                this.ShowNumCreditCardError = true;
            else
                this.ShowNumCreditCardError = string.IsNullOrEmpty(NumCreditCard.Trim());
            if (!this.ShowNumCreditCardError)
            {
                if (!Regex.IsMatch(this.NumCreditCard, @"^[0-9]*$"))
                {
                    this.ShowNumCreditCardError = true;
                    this.NumCreditCardError = ERROR_MESSAGES.BAD_NUMBER;
                }
                else if (this.NumCreditCard.Length != 16)
                {
                    this.ShowNumCreditCardError = true;
                    this.NumCreditCardError = ERROR_MESSAGES.BAD_NUM_CREDIT_CARD;
                }
            }
            else
                this.NumCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region NumCode
        private string numCode;
        public string NumCode
        {
            get { return numCode; }
            set
            {
                numCode = value;
                OnPropertyChanged("NumCode");
            }
        }

        private bool showNumCodeError;
        public bool ShowNumCodeError
        {
            get => showNumCodeError;
            set
            {
                showNumCodeError = value;
                OnPropertyChanged("ShowNumCodeError");
            }
        }

        private string numCodeError;
        public string NumCodeError
        {
            get => numCodeError;
            set
            {
                numCodeError = value;
                OnPropertyChanged("NumCodeError");
            }
        }

        private void ValidateNumCode()
        {
            if (NumCode == null)
                this.ShowNumCodeError = true;
            else
                this.ShowNumCodeError = string.IsNullOrEmpty(NumCode.Trim());
            if (!this.ShowNumCodeError)
            {
                if (!Regex.IsMatch(this.NumCode, @"^[0-9]*$"))
                {
                    this.ShowNumCodeError = true;
                    this.NumCodeError = ERROR_MESSAGES.BAD_NUMBER;
                }
                else if (this.NumCode.Length != 3)
                {
                    this.ShowNumCodeError = true;
                    this.NumCodeError = ERROR_MESSAGES.BAD_NUM_CODE;
                }
            }
            else
                this.NumCodeError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region ValidityCreditCard
        private DateTime validityCreditCard;
        public DateTime ValidityCreditCard
        {
            get { return validityCreditCard; }
            set
            {
                validityCreditCard = value;
                OnPropertyChanged("ValidityCreditCard");
            }
        }

        private bool showValidityCreditCardError;
        public bool ShowValidityCreditCardError
        {
            get => showValidityCreditCardError;
            set
            {
                showValidityCreditCardError = value;
                OnPropertyChanged("ShowValidityCreditCardError");
            }
        }

        private string validityCreditCardError;
        public string ValidityCreditCardError
        {
            get => validityCreditCardError;
            set
            {
                validityCreditCardError = value;
                OnPropertyChanged("ValidityCreditCardError");
            }
        }

        private void ValidateValidityCreditCard()
        {
            if (this.ValidityCreditCard == DateTime.MinValue)
            {
                this.ShowValidityCreditCardError = true;
                this.ValidityCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;
            }
            else if (this.ValidityCreditCard < DateTime.Now.Date)
            {
                this.ShowValidityCreditCardError = true;
                this.ValidityCreditCardError = ERROR_MESSAGES.BAD_VALIDITY_CREDIT_CARD;
            }
            else
                this.ShowValidityCreditCardError = false;
        }
        #endregion

        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }


        public App App { get; set; }

        public UserDetailsViewModel()
        {
            this.App = (App)Application.Current;
            this.Cities = new List<string>(App.Cities);

            this.PhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.AddressError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NumCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NumCodeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ValidityCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;

            this.Phone = App.CurrentUser.UserPhone;
            this.Address = App.CurrentUser.UserAddress;
            this.City = App.CurrentUser.UserCity;
            this.NumCreditCard = App.CurrentUser.NumCreditCard;
            this.NumCode = App.CurrentUser.NumCode;
            this.ValidityCreditCard = App.CurrentUser.ValidityCreditCard.Value;
        }

        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePhone();
            ValidateAddress();
            ValidateCity();
            ValidateNumCreditCard();
            ValidateNumCode();
            ValidateValidityCreditCard();

            //Check if any validation failed
            if (ShowPhoneError || ShowAddressError || ShowCityError || ShowNumCreditCardError ||
                ShowNumCodeError || ShowValidityCreditCardError)
                return false;
            return true;
        }

        public ICommand UpdateCommand => new Command(Update);
        public async void Update()
        {
            if (!ValidateForm())
                return;

            ServerStatus = "מעדכן פרטים אישיים...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
            //Thread.Sleep(2000);

            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            bool isUpdatedUser = await proxy.UpdateUser(App.CurrentUser, Phone, Address, City, NumCreditCard, NumCode, ValidityCreditCard);
            await App.Current.MainPage.Navigation.PopModalAsync();

            if (isUpdatedUser)
            {
                App.CurrentUser.UserPhone = Phone;
                App.CurrentUser.UserAddress = Address;
                App.CurrentUser.UserCity = City;
                App.CurrentUser.NumCreditCard = NumCreditCard;
                App.CurrentUser.NumCode = NumCode;
                App.CurrentUser.ValidityCreditCard = ValidityCreditCard;
                await App.Current.MainPage.DisplayAlert("עדכון בוצע בהצלחה", "", "אישור", FlowDirection.RightToLeft);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "לא ניתן היה לעדכן פרטים", "בסדר", FlowDirection.RightToLeft);
            }
        }

    }
}
