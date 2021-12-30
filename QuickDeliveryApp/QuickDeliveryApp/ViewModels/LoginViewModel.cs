using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QuickDeliveryApp.Services;
using QuickDeliveryApp.Models;
using System.Threading.Tasks;

namespace QuickDeliveryApp.ViewModels
{
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
            this.TitleText = "Login";
            this.GoToText = "To register";
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
                this.IsRegister = false;
                this.TitleText = "Login";
                this.GoToText = "To register";
            }   
            else
            {
                this.IsRegister = true;
                this.TitleText = "Register";
                this.GoToText = "Back to login";
            }
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
            if ((FName == "") || (LName == "") || (Email == "") ||
                (Password == "") || (Phone == "") || (Address == "") ||
                (City == "") || (NumCreditCard == "") || (NumCode == ""))
            //תאריך לידה ותוקף כקטיס אשראי
            {
                await App.Current.MainPage.DisplayAlert("QuickDelivery", "Please fill all the fields", "Ok");
                return false;
            }
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

            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            bool registerSucceed = await proxy.RegisterUser(user);

            if (registerSucceed)
            {
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("QuickDelivery", "Register is failed, please try again", "Ok");
            }
            //EntryEmail = "";
            //EntryNickName = "";
            //EntryPass = "";

            return registerSucceed;
        }

        public async void Login()
        {
            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
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
                App theApp = (App)App.Current;
                theApp.CurrentUser = user;
                await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("היפ היפ הוריי", "התחברת הצליחה", "בסדר");
            }
        }
    }
}
