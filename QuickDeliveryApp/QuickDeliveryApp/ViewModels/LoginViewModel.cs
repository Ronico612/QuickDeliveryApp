using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.Models;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using QuickDeliveryApp.Views;
using System.Threading;

namespace QuickDeliveryApp.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string BAD_NUM_CREDIT_CARD = "מספר כרטיס אשראי לא תקין";
        public const string BAD_NUMBER = "השדה חייב להכיל רק מספרים";
    }

    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string titleText;
        public string TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value;
                OnPropertyChanged("TitleText");
            }
        }
        private string goToText;
        public string GoToText
        {
            get { return goToText; }
            set
            {
                goToText = value;
                OnPropertyChanged("GoToText");
            }
        }

        #region FName
        private string fName;
        public string FName
        {
            get { return fName; }
            set
            {
                fName = value;
                OnPropertyChanged("FName");
            }
        }

        private bool showFNameError;
        public bool ShowFNameError
        {
            get => showFNameError;
            set
            {
                showFNameError = value;
                OnPropertyChanged("ShowFNameError");
            }
        }

        private string fNameError;
        public string FNameError
        {
            get => fNameError;
            set
            {
                fNameError = value;
                OnPropertyChanged("FNameError");
            }
        }

        private void ValidateFName()
        {
            if (FName == null)
                this.ShowFNameError = true;
            else
                this.ShowFNameError = string.IsNullOrEmpty(this.FName.Trim());
        }
        #endregion

        #region LName
        private string lName;
        public string LName
        {
            get { return lName; }
            set
            {
                lName = value;
                OnPropertyChanged("LName");
            }
        }

        private bool showLNameError;
        public bool ShowLNameError
        {
            get => showLNameError;
            set
            {
                showLNameError = value;
                OnPropertyChanged("ShowLNameError");
            }
        }

        private string lNameError;
        public string LNameError
        {
            get => lNameError;
            set
            {
                lNameError = value;
                OnPropertyChanged("LNameError");
            }
        }

        private void ValidateLName()
        {
            if (LName == null)
                this.ShowLNameError = true;
            else
                this.ShowLNameError = string.IsNullOrEmpty(this.LName.Trim());
        }
        #endregion

        #region Email
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private bool showEmailError;
        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            if (Email == null)
                this.ShowEmailError = true;
            else
                this.ShowEmailError = string.IsNullOrEmpty(Email.Trim());

            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Password
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool showPasswordError;
        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string passwordError;
        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            if (Password == null)
                this.ShowPasswordError = true;
            else
                this.ShowPasswordError = string.IsNullOrEmpty(this.Password.Trim());
        }
        #endregion

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
            }
            else
                this.PhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region BirthDate
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        private bool showBirthDateError;
        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private string birthDateError;
        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private void ValidateBirthDate()
        {
            if (this.BirthDate == DateTime.MinValue)
                this.ShowBirthDateError = true;
            else
                this.ShowBirthDateError = false;
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
                this.ShowValidityCreditCardError = true;
            else
                this.ShowValidityCreditCardError = false;
        }
        #endregion

        private bool isRegister;
        public bool IsRegister
        {
            get { return isRegister; }
            set
            {
                isRegister = value;
                OnPropertyChanged("IsRegister");
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public LoginViewModel()
        {
            IsRegister = false;
            this.TitleText = "התחברות";
            this.GoToText = "להרשמה";

            this.FNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.LNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.AddressError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NumCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NumCodeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ValidityCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;

            SubmitCommand = new Command(OnSubmit);
        }

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

        public ICommand GotoCommand => new Command(Goto);
        public void Goto()
        {
            if (this.IsRegister)
            {
                ClearFields();
                this.IsRegister = false;
                this.TitleText = "התחברות";
                this.GoToText = "להרשמה";
            }   
            else
            {
                this.IsRegister = true;
                this.TitleText = "הרשמה";
                this.GoToText = "חזרה להתחברות";
            }
        }

        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateFName();
            ValidateLName();
            ValidateEmail();
            ValidatePassword();
            ValidatePhone();
            ValidateBirthDate();
            ValidateAddress();
            ValidateCity();
            ValidateNumCreditCard();
            ValidateNumCode();
            ValidateValidityCreditCard();

            //Check if any validation failed
            if (showFNameError || ShowLNameError || ShowEmailError ||
                ShowPasswordError || ShowPhoneError || ShowBirthDateError ||
                ShowAddressError || ShowCityError || ShowNumCreditCardError || 
                ShowNumCodeError || ShowValidityCreditCardError)
                return false;
            return true;
        }

        public async void OnSubmit()
        {
            if (this.IsRegister)
            {
                if (!(await Register()))
                    return;
            }
            Login();
        }

        public async Task<bool> Register()
        {
            if (!ValidateForm())
            {
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
                return false;
            }

            ServerStatus = "בודק תקינות מייל...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
            Thread.Sleep(1000);

            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            bool isEmailExist = await proxy.IsUserEmailExistAsync(Email);
            if (isEmailExist)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה בשמירת נתונים", "אימייל זה כבר קיים במערכת, נסה שנית", "אישור", FlowDirection.RightToLeft);
                return false;
            }

            ServerStatus = "מבצע הרשמה...";
            Thread.Sleep(1000);
            User user = new User();
            user.UserFname = FName;
            user.UserLname = LName;
            user.UserEmail = Email;
            user.UserPassword = Password;
            user.UserPhone = Phone;
            user.UserBirthDate = birthDate;
            user.UserAddress = Address;
            user.UserCity = City;
            user.NumCreditCard = NumCreditCard;
            user.NumCode = NumCode;
            user.ValidityCreditCard = ValidityCreditCard;

           
            bool registerSucceed = await proxy.RegisterUser(user);
            await App.Current.MainPage.Navigation.PopModalAsync();
            
            if (registerSucceed)
            {

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "ההרשמה נכשלה", "בסדר", FlowDirection.RightToLeft);
            }
            return registerSucceed;
        }

        public async void Login()
        {
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
            Thread.Sleep(1000);
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(Email, Password);
            if (user == null)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                Thread.Sleep(2000);
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;
                //await App.Current.MainPage.DisplayAlert("היפ הופ הוריי", "התחברת בהצלחה למערכת", "בסדר");
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
                theTabs.AddTab(theTabs.personalArea);
                theTabs.CurrentTab(theTabs.personalArea);
                theTabs.RemoveTab(theTabs.login);

                await App.Current.MainPage.Navigation.PopModalAsync();
                ///////////////////////////////////////////////
                theTabs.AddTab(theTabs.shopManager);

                ClearFields();
            }
        }

        private void ClearFields()
        {
            FName = "";
            LName = "";
            Email = "";
            Password = "";
            Phone = "";
            BirthDate = DateTime.Today;
            Address = "";
            City = "";
            NumCreditCard = "";
            NumCode = "";
            ValidityCreditCard = DateTime.Today;
            ShowFNameError = false;
            ShowLNameError = false;
            ShowEmailError = false;
            ShowPasswordError = false;
            ShowPhoneError = false;
            ShowBirthDateError = false;
            ShowAddressError = false;
            ShowCityError = false;
            ShowNumCreditCardError = false;
            ShowNumCodeError = false;
            ShowValidityCreditCardError = false;
        }
    }
}
