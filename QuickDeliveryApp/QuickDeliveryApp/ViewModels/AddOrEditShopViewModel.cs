using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace QuickDeliveryApp.ViewModels
{
    class AddOrEditShopViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Shop shop;
        public Shop Shop
        {
            get
            {
                return this.shop;
            }
            set
            {
                if (this.shop != value)
                {
                    this.shop = value;
                    OnPropertyChanged("Shop");
                }
            }
        }

        private bool isAdded;
        public bool IsAdded
        {
            get
            {
                return this.isAdded;
            }
            set
            {
                if (this.isAdded != value)
                {

                    this.isAdded = value;
                    OnPropertyChanged("IsAddded");
                }
            }
        }

        #region ShopName
        private string shopName;
        public string ShopName
        {
            get { return shopName; }
            set
            {
                shopName = value;
                OnPropertyChanged("ShopName");
            }
        }

        private bool showShopNameError;
        public bool ShowShopNameError
        {
            get => showShopNameError;
            set
            {
                showShopNameError = value;
                OnPropertyChanged("ShowShopNameError");
            }
        }

        private string shopNameError;
        public string ShopNameError
        {
            get => shopNameError;
            set
            {
                shopNameError = value;
                OnPropertyChanged("ShopNameError");
            }
        }

        private void ValidateShopName()
        {
            if (ShopName == null)
                this.ShowShopNameError = true;
            else
                this.ShowShopNameError = string.IsNullOrEmpty(ShopName.Trim());
        }
        #endregion

        #region ShopAdress
        private string shopAdress;
        public string ShopAdress
        {
            get { return shopAdress; }
            set
            {
                shopAdress = value;
                OnPropertyChanged("ShopAdress");
            }
        }

        private bool showShopAdressError;
        public bool ShowShopAdressError
        {
            get => showShopAdressError;
            set
            {
                showShopAdressError = value;
                OnPropertyChanged("ShowShopAdressError");
            }
        }

        private string shopAdressError;
        public string ShopAdressError
        {
            get => shopAdressError;
            set
            {
                shopAdressError = value;
                OnPropertyChanged("ShopAdressError");
            }
        }

        private void ValidateShopAdress()
        {
            if (ShopAdress == null)
                this.ShowShopAdressError = true;
            else
                this.ShowShopAdressError = string.IsNullOrEmpty(ShopAdress.Trim());
        }
        #endregion

        #region ShopCity
        private string shopCity;
        public string ShopCity
        {
            get { return shopCity; }
            set
            {
                shopCity = value;
                OnPropertyChanged("ShopCity");
            }
        }

        private bool showShopCityError;
        public bool ShowShopCityError
        {
            get => showShopCityError;
            set
            {
                showShopCityError = value;
                OnPropertyChanged("ShowShopCityError");
            }
        }

        private string shopCityError;
        public string ShopCityError
        {
            get => shopCityError;
            set
            {
                shopCityError = value;
                OnPropertyChanged("ShopCityError");
            }
        }

        private void ValidateShopCity()
        {
            if (ShopCity == null)
                this.ShowShopCityError = true;
            else
                this.ShowShopCityError = string.IsNullOrEmpty(ShopCity.Trim());
        }
        #endregion

        #region ShopPhone
        private string shopPhone;
        public string ShopPhone
        {
            get { return shopPhone; }
            set
            {
                shopPhone = value;
                OnPropertyChanged("ShopPhone");
            }
        }

        private bool showShopPhoneError;
        public bool ShowShopPhoneError
        {
            get => showShopPhoneError;
            set
            {
                showShopPhoneError = value;
                OnPropertyChanged("ShowShopPhoneError");
            }
        }

        private string shopPhoneError;
        public string ShopPhoneError
        {
            get => shopPhoneError;
            set
            {
                shopPhoneError = value;
                OnPropertyChanged("ShopPhoneError");
            }
        }

        private void ValidateShopPhone()
        {
            if (ShopPhone == null)
                this.ShowShopPhoneError = true;
            else
                this.ShowShopPhoneError = string.IsNullOrEmpty(ShopPhone.Trim());

            if (!this.ShowShopPhoneError)
            {
                if (!Regex.IsMatch(this.ShopPhone, @"^[0-9]*$"))
                {
                    this.ShowShopPhoneError = true;
                    this.ShopPhoneError = ERROR_MESSAGES.BAD_NUMBER;
                }
                else if (!Regex.IsMatch(this.ShopPhone, @"^\+?(972|0)(\-)?0?(([23489]{1}\d{7})|[5]{1}\d{8})$"))
                {
                    this.ShowShopPhoneError = true;
                    this.ShopPhoneError = ERROR_MESSAGES.BAD_PHONE;
                }
            }
            else
                this.ShopPhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region ShopManagerEmail
        private string shopManagerEmail;
        public string ShopManagerEmail
        {
            get { return shopManagerEmail; }
            set
            {
                shopManagerEmail = value;
                OnPropertyChanged("ShopManagerEmail");
            }
        }

        private bool showShopManagerEmailError;
        public bool ShowShopManagerEmailError
        {
            get => showShopManagerEmailError;
            set
            {
                showShopManagerEmailError = value;
                OnPropertyChanged("ShowShopManagerEmailError");
            }
        }

        private string shopManagerEmailError;
        public string ShopManagerEmailError
        {
            get => shopManagerEmailError;
            set
            {
                shopManagerEmailError = value;
                OnPropertyChanged("ShopManagerEmailError");
            }
        }

        private void ValidateShopManagerEmail()
        {
            if (ShopManagerEmail == null)
                this.ShowShopManagerEmailError = true;
            else
                this.ShowShopManagerEmailError = string.IsNullOrEmpty(ShopManagerEmail.Trim());

            if (!this.ShowShopManagerEmailError)
            {
                if (!Regex.IsMatch(ShopManagerEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowShopManagerEmailError = true;
                    this.ShopManagerEmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.ShopManagerEmailError = ERROR_MESSAGES.REQUIRED_FIELD;
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

        public AddOrEditShopViewModel(Shop s)
        {
            Init(s);
            this.ShopNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopAdressError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopCityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopPhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopManagerEmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        private async void Init(Shop s)
        {
            if (s == null)  // הוספת חנות
            {
                isAdded = true;
                this.Shop = new Shop();
            }
            else
            {
                this.Shop = s;
                this.ShopName = s.ShopName;
                this.ShopAdress = s.ShopAdress;
                this.ShopCity = s.ShopCity;
                this.ShopPhone = s.ShopPhone;

                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                User user = await proxy.GetUserAsync((int)s.ShopManagerId);
                if (user != null)
                    this.ShopManagerEmail = user.UserEmail;
            }
        }

        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateShopName();
            ValidateShopAdress();
            ValidateShopCity();
            ValidateShopPhone();
            ValidateShopManagerEmail();

            //Check if any validation failed
            if (ShowShopNameError || showShopAdressError || ShowShopCityError || ShowShopPhoneError || showShopManagerEmailError)
                return false;
            return true;
        }

        public ICommand ChooseImageCommand => new Command(ChooseImage);
        public async void ChooseImage()
        {

        }

        public ICommand AddOrEditCommand => new Command(AddOrEdit);
        public async void AddOrEdit()
        {
            if (!ValidateForm())
                return;

            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            bool isEmailExist = await proxy.IsUserEmailExistAsync(ShopManagerEmail);
            if (!isEmailExist)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "מנהל החנות אינו רשום במערכת", "בסדר", FlowDirection.RightToLeft);
                return;
            }

            if (IsAdded)
            {
                ServerStatus = "מוסיף חנות...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

                Thread.Sleep(1000);
                Shop newShop = new Shop();
                newShop.ShopName = ShopName;
                newShop.ShopAdress = ShopAdress;
                newShop.ShopCity = ShopCity;
                newShop.ShopPhone = shopPhone;
                App app = (App)Application.Current;

                int smId = await proxy.AddShopManagerAsync(ShopManagerEmail);
                if (smId == -1)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת החנות נכשלה. בדקו אם מנהל החנות קיים עבור חנות אחרת", "בסדר", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    newShop.ShopManagerId = smId;
                    int newShopId = await proxy.AddShopAsync(newShop);
                    await App.Current.MainPage.Navigation.PopModalAsync();

                    if (newShopId >= 0)
                    {
                        await App.Current.MainPage.DisplayAlert("", "החנות נוספה בהצלחה", "אישור", FlowDirection.RightToLeft);
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת החנות נכשלה", "בסדר", FlowDirection.RightToLeft);
                    }
                }
            }
            else // Update
            {
                ServerStatus = "מעדכן פרטי חנות...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
                Thread.Sleep(2000);

                int shopManagerId = (int)Shop.ShopManagerId;
                User user = await proxy.GetUserAsync(shopManagerId);
                string originalShopManagerEmail = "";
                if (user != null)
                    originalShopManagerEmail = user.UserEmail;

                if (originalShopManagerEmail != shopManagerEmail)
                {
                    int smId = await proxy.AddShopManagerAsync(ShopManagerEmail);
                    if (smId == -1)
                    {
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת החנות נכשלה. בדקו אם מנהל החנות קיים עבור חנות אחרת", "בסדר", FlowDirection.RightToLeft);
                        return;
                    }
                    else
                        shopManagerId = smId;
                }

                bool isUpdatedShop = await proxy.UpdateShop(this.Shop.ShopId, ShopName, ShopAdress, ShopCity, ShopPhone, shopManagerId);
                await App.Current.MainPage.Navigation.PopModalAsync();

                if (isUpdatedShop)
                {
                    if (originalShopManagerEmail != shopManagerEmail)
                        await proxy.DeleteShopManager((int)Shop.ShopManagerId);
                    await App.Current.MainPage.DisplayAlert("החנות התעדכנה בהצלחה", "", "אישור", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    if (originalShopManagerEmail != shopManagerEmail)
                        await proxy.DeleteShopManager(shopManagerId);
                    await App.Current.MainPage.DisplayAlert("שגיאה", "לא ניתן היה לעדכן פרטים", "בסדר", FlowDirection.RightToLeft);
                }
            }
        }
    }
}
