using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.Models;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using QuickDeliveryApp.Views;
using System.Linq;
using System.Collections.ObjectModel;
using QuickDeliveryApp.DTO;

namespace QuickDeliveryApp.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string BAD_CITY = "שם העיר לא תקין";
        public const string BAD_STREET = "רחוב לא תקין";
        public const string BAD_STREET_NUM = "מספר בית לא תקין";
        public const string BAD_PHONE = "מספר טלפון לא תקין";
        public const string BAD_NUM_CREDIT_CARD = "מספר כרטיס אשראי לא תקין";
        public const string BAD_NUM_CODE = "מספר סודי לא תקין";
        public const string BAD_VALIDITY_CREDIT_CARD = "כרטיס לא בתוקף";
        public const string BAD_NUMBER = "השדה חייב להכיל רק מספרים";
        public const string PRODUCT_EXIST = "שם המוצר כבר קיים";
    }

    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                ValidateFName();
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
                ValidateLName();
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
                ValidateEmail();
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
                ValidatePassword();
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
                ValidatePhone();
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

        #region BirthDate
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                ValidateBirthDate();
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

        #region City
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    ValidateCity();
                    OnCityChanged(value);
                    OnPropertyChanged("City");
                }
            }
        }

        private string selectedCityItem;
        public string SelectedCityItem
        {
            get => selectedCityItem;
            set
            {
                selectedCityItem = value;
                OnPropertyChanged("SelectedCityItem");
            }
        }

        public ICommand SelectedCity => new Command<string>(OnSelectedCity);
        public void OnSelectedCity(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                this.City = city;
                this.IsStreetEnabled = true;
                this.ShowCities = false;
            }
        }

        private bool showCities;
        public bool ShowCities
        {
            get => showCities;
            set
            {
                showCities = value;
                OnPropertyChanged("ShowCities");
            }
        }

        public void OnCityChanged(string search)
        {
            this.Street = "";
            this.ShowStreets = false;
            this.FilteredStreets.Clear();
            this.IsStreetEnabled = false;

            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }

            if (this.allCities == null)
                return;

            //Filter the list of cities based on the search term
            if (String.IsNullOrWhiteSpace(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                List<string> cityList = this.allCities.Where(c => c.Contains(search)).OrderBy(c => c).ToList();
                this.FilteredCities = new ObservableCollection<string>(cityList);
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
            this.ShowCityError = string.IsNullOrEmpty(this.City);
            if (!this.ShowCityError)
            {
                string city = this.allCities.Where(c => c == this.City).FirstOrDefault();
                if (string.IsNullOrEmpty(city))
                {
                    this.ShowCityError = true;
                    this.CityError = ERROR_MESSAGES.BAD_CITY;
                }
            }
            else
                this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        #endregion

        #region Street

        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnStreetChanged(value);
                ValidateStreet();
                OnPropertyChanged("Street");
            }
        }

        private bool showStreets;
        public bool ShowStreets
        {
            get => showStreets;
            set
            {
                showStreets = value;
                OnPropertyChanged("ShowStreets");
            }
        }

        private string selectedStreetItem;
        public string SelectedStreetItem
        {
            get => selectedStreetItem;
            set
            {
                selectedStreetItem = value;
                OnPropertyChanged("SelectedStreetItem");
            }
        }

        public ICommand SelectedStreet => new Command<string>(OnSelectedStreet);
        public void OnSelectedStreet(string street)
        {
            if (!string.IsNullOrEmpty(street))
            {
                this.ShowStreets = false;
                this.Street = street;
            }
        }

        private bool showStreetError;
        public bool ShowStreetError
        {
            get => showStreetError;
            set
            {
                showStreetError = value;
                OnPropertyChanged("ShowStreetError");
            }
        }

        private string streetError;
        public string StreetError
        {
            get => streetError;
            set
            {
                streetError = value;
                OnPropertyChanged("StreetError");
            }
        }

        private bool isStreetEnabled;
        public bool IsStreetEnabled
        {
            get => isStreetEnabled;
            set
            {
                isStreetEnabled = value;
                OnPropertyChanged("IsStreetEnabled");
            }
        }

        private void ValidateStreet()
        {
            this.ShowStreetError = string.IsNullOrEmpty(this.Street);
            if (!this.ShowStreetError)
            {
                Street street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
                if (street == null)
                {
                    this.ShowStreetError = true;
                    this.StreetError = ERROR_MESSAGES.BAD_STREET;
                }
            }
            else
                this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        public void OnStreetChanged(string search)
        {
            if (this.Street != this.SelectedStreetItem)
            {
                this.ShowStreets = true;
                this.SelectedStreetItem = null;
            }

            if (this.allStreets == null)
                return;

            //Filter the list of streets based on the search term
            if (String.IsNullOrWhiteSpace(search))
            {
                this.ShowStreets = false;
                this.FilteredStreets.Clear();
            }
            else
            {
                List<Street> streetList = this.allStreets.Where(s => s.street_name.Contains(search) && s.city_name == this.City).OrderBy(s => s.street_name).ToList();
                this.FilteredStreets = new ObservableCollection<string>(streetList.Select(s => s.street_name));
            }
        }
        #endregion

        #region StreetNum
        private bool showStreetNumError;
        public bool ShowStreetNumError
        {
            get => showStreetNumError;
            set
            {
                showStreetNumError = value;
                OnPropertyChanged("ShowStreetNumError");
            }
        }

        private string streetNum;
        public string StreetNum
        {
            get => streetNum;
            set
            {
                streetNum = value;
                ValidateStreetNum();
                OnPropertyChanged("StreetNum");
            }
        }

        private string streetNumError;
        public string StreetNumError
        {
            get => streetNumError;
            set
            {
                streetNumError = value;
                OnPropertyChanged("StreetNumError");
            }
        }

        private void ValidateStreetNum()
        {
            this.ShowStreetNumError = string.IsNullOrEmpty(this.StreetNum);
            int num;
            if (!this.ShowStreetNumError)
            {
                if (this.StreetNum.StartsWith("0") || !int.TryParse(this.StreetNum, out num) || num <= 0)
                {
                    this.ShowStreetNumError = true;
                    this.StreetNumError = ERROR_MESSAGES.BAD_STREET_NUM;
                }
            }
            else
                this.StreetNumError = ERROR_MESSAGES.REQUIRED_FIELD;
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
                ValidateNumCreditCard();
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
                ValidateNumCode();
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
                ValidateValidityCreditCard();
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
            if (this.ValidityCreditCard.Date <= DateTime.Now.Date)
            {
                this.ShowValidityCreditCardError = true;
            }
            else
                this.ShowValidityCreditCardError = false;
        }
        #endregion

        private List<string> allCities;

        private ObservableCollection<string> filteredCities;
        public ObservableCollection<string> FilteredCities
        {
            get
            {
                return this.filteredCities;
            }
            set
            {
                if (this.filteredCities != value)
                {

                    this.filteredCities = value;
                    OnPropertyChanged("FilteredCities");
                }
            }
        }

        private List<Street> allStreets;

        private ObservableCollection<string> filteredStreets;
        public ObservableCollection<string> FilteredStreets
        {
            get
            {
                return this.filteredStreets;
            }
            set
            {
                if (this.filteredStreets != value)
                {

                    this.filteredStreets = value;
                    OnPropertyChanged("FilteredStreets");
                }
            }
        }

        public ICommand SubmitCommand { protected set; get; }

        public ICommand GotoCommand => new Command(Goto);

        public LoginViewModel()
        {
            App app = (App)Application.Current;

            IsRegister = false;
            this.TitleText = "התחברות";
            this.GoToText = "להרשמה";

            allCities = app.Cities;
            this.FilteredCities = new ObservableCollection<string>();

            this.allStreets = app.Streets;
            this.FilteredStreets = new ObservableCollection<string>();

            this.FNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.LNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.StreetError = ERROR_MESSAGES.BAD_STREET;
            this.StreetNumError = ERROR_MESSAGES.BAD_STREET_NUM;
            this.NumCreditCardError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NumCodeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ValidityCreditCardError = ERROR_MESSAGES.BAD_VALIDITY_CREDIT_CARD;

            SubmitCommand = new Command(OnSubmit);
        }

        public void Goto()
        {
            ClearFields();
            if (this.IsRegister)
            {
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
            ValidateCity();
            ValidateStreet();
            ValidateStreetNum();
            ValidateNumCreditCard();
            ValidateNumCode();
            ValidateValidityCreditCard();

            //Check if any validation failed
            if (showFNameError || ShowLNameError || ShowEmailError ||
                ShowPasswordError || ShowPhoneError || ShowBirthDateError ||
                ShowCityError || ShowStreetError || ShowStreetNumError || ShowNumCreditCardError || 
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
                return false;
            }

            ServerStatus = "בודק תקינות מייל...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            bool isEmailExist = await proxy.IsUserEmailExistAsync(Email);
            if (isEmailExist)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה בשמירת נתונים", "אימייל זה כבר קיים במערכת, נסה שנית", "אישור", FlowDirection.RightToLeft);
                return false;
            }

            ServerStatus = "מבצע הרשמה...";
            User user = new User();
            user.UserFname = FName;
            user.UserLname = LName;
            user.UserEmail = Email;
            user.UserPassword = Password;
            user.UserPhone = Phone;
            user.UserBirthDate = birthDate;
            user.UserCity = City;
            user.UserAddress = Street;
            //user.UserStreetNum = StreetNum;
            user.NumCreditCard = NumCreditCard;
            user.NumCode = NumCode;
            user.ValidityCreditCard = ValidityCreditCard;
                       
            bool registerSucceed = await proxy.RegisterUser(user);
            await App.Current.MainPage.Navigation.PopModalAsync();
            
            if (registerSucceed)
            {
                this.IsRegister = false;
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
            App theApp = (App)App.Current;
            bool goToPaymentAfterLogin = theApp.goToPaymentAfterLogin;
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            theApp.goToPaymentAfterLogin = goToPaymentAfterLogin;
            User user = await proxy.LoginAsync(Email, Password);
            if (user == null)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
            }
            else
            {
                ServerStatus = "קורא נתונים...";
                theApp.CurrentUser = user;
                //await App.Current.MainPage.DisplayAlert("היפ הופ הוריי", "התחברת בהצלחה למערכת", "בסדר");
                NavigationPage tabbed = (NavigationPage)Application.Current.MainPage;
                TheMainTabbedPage theTabs = (TheMainTabbedPage)tabbed.CurrentPage;
                theTabs.AddTab(theTabs.personalArea);
                if (theApp.goToPaymentAfterLogin)
                {
                    Page p = new Pay();
                    p.Title = "ביצוע הזמנה";
                    p.BindingContext = new PayViewModel();
                    await tabbed.Navigation.PushAsync(p);
                    theTabs.CurrentTab(theTabs.shoppingCart);
                    theApp.goToPaymentAfterLogin = false;
                }
                else
                {
                    theTabs.CurrentTab(theTabs.personalArea);
                }
                theTabs.RemoveTab(theTabs.login);

                await App.Current.MainPage.Navigation.PopModalAsync();

                if (user.IsAdmin)
                    theTabs.AddTab(theTabs.admin);

                if (theApp.AllShops.Where(s => s.ShopManagerId == theApp.CurrentUser.UserId).FirstOrDefault() != null)
                    theTabs.AddTab(theTabs.shopManager);

                if (await proxy.IsDeliveyPerson(theApp.CurrentUser.UserId))
                    theTabs.AddTab(theTabs.deliveryPerson);

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
            City = "";
            Street = "";
            StreetNum = "";
            NumCreditCard = "";
            NumCode = "";
            ValidityCreditCard = DateTime.Today;
            ShowFNameError = false;
            ShowLNameError = false;
            ShowEmailError = false;
            ShowPasswordError = false;
            ShowPhoneError = false;
            ShowBirthDateError = false;
            ShowCityError = false;
            ShowStreetError = false;
            ShowStreetNumError = false;
            ShowNumCreditCardError = false;
            ShowNumCodeError = false;
            ShowValidityCreditCardError = false;
        }
    }
}
