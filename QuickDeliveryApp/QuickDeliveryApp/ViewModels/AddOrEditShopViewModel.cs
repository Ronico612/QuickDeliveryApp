using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        #region ImgSource
        private string imgSource;

        public string ImgSource
        {
            get => imgSource;
            set
            {
                imgSource = value;
                ValidateImgSource();
                OnPropertyChanged("ImgSource");
            }
        }

        private bool showImgSourceError;
        public bool ShowImgSourceError
        {
            get => showImgSourceError;
            set
            {
                showImgSourceError = value;
                OnPropertyChanged("ShowImgSourceError");
            }
        }

        private string imgSourceError;
        public string ImgSourceError
        {
            get => imgSourceError;
            set
            {
                imgSourceError = value;
                OnPropertyChanged("ImgSourceError");
            }
        }

        private void ValidateImgSource()
        {
            if (((!string.IsNullOrEmpty(ImgSource)) && (ImgSource.Contains("EmptyImg")) && (this.imageFileResult == null)) || ((this.imageFileResult != null) && (this.imageFileResult.FullPath.Contains("EmptyImg"))))
                this.ShowImgSourceError = true;
            else
                this.ShowImgSourceError = false;

            this.ImgSourceError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        #endregion

        #region ShopName
        private string shopName;
        public string ShopName
        {
            get { return shopName; }
            set
            {
                shopName = value;
                ValidateShopName();
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
                ValidateShopAdress();
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
                OnCityChanged(value);
                ValidateShopCity();
                OnPropertyChanged("ShopCity");
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
            if (city != null)
            {
                this.ShowCities = false;
                this.ShopCity = city;
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
            if (this.ShopCity != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }
            //Filter the list of cities based on the search term
            if (this.allCities == null)
                return;

            if (String.IsNullOrWhiteSpace(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                foreach (string city in this.allCities)
                {
                    if (!this.FilteredCities.Contains(city) && city.Contains(search))
                        this.FilteredCities.Add(city);
                    else if (this.FilteredCities.Contains(city) && !city.Contains(search))
                        this.FilteredCities.Remove(city);
                }
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
            this.ShowShopCityError = string.IsNullOrEmpty(this.ShopCity);
            if (!this.ShowShopCityError)
            {
                string city = this.allCities.Where(c => c == this.ShopCity).FirstOrDefault();
                if (string.IsNullOrEmpty(city))
                {
                    this.ShowShopCityError = true;
                    this.ShopCityError = ERROR_MESSAGES.BAD_CITY;
                }
            }
            else
                this.ShopCityError = ERROR_MESSAGES.REQUIRED_FIELD;
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
                ValidateShopPhone();
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
                ValidateShopManagerEmail();
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
            App app = (App)Application.Current;
            allCities = app.Cities;
            this.FilteredCities = new ObservableCollection<string>();
            Init(s);
            this.imageFileResult = null;
            this.ShopNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopAdressError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopCityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopPhoneError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ShopManagerEmailError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ImgSourceError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        private async void Init(Shop s)
        {
            if (s == null)  // הוספת חנות
            {
                isAdded = true;
                this.Shop = new Shop();
                this.ImgSource = this.Shop.EmptyImgSource;
                this.ShowImgSourceError = false;
            }
            else
            {
                this.Shop = s;
                this.ImgSource = s.ImgSource;
                this.ShopName = s.ShopName;
                this.ShopAdress = s.ShopAdress;
                this.ShopCity = s.ShopCity;
                this.ShopPhone = s.ShopPhone;

                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                if (s.ShopManagerId != null)
                {
                    User user = await proxy.GetUserAsync((int)s.ShopManagerId);
                    if (user != null)
                        this.ShopManagerEmail = user.UserEmail;
                }
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
            ValidateImgSource();

            //Check if any validation failed
            if (ShowShopNameError || ShowShopAdressError || ShowShopCityError || ShowShopPhoneError || ShowShopManagerEmailError || ShowImgSourceError)
                return false;
            return true;
        }

        #region Upload Image Code
        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;

        public ICommand ChooseImageCommand => new Command(ChooseImage);
        public async void ChooseImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "בחר תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (imgSource != null)
                    ShowImgSourceError = false;
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion

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

            bool successImg = true;
            if (IsAdded)
            {
                ServerStatus = "מוסיף חנות...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

                Thread.Sleep(500);
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
                        if (this.imageFileResult != null)
                        {
                            ServerStatus = "מעלה תמונה...";

                            successImg = await proxy.UploadShopImage(new FileInfo()
                            {
                                Name = this.imageFileResult.FullPath
                            }, $"{newShopId}.png");
                        }
                        ServerStatus = "שומר נתונים...";

                        if (successImg)
                            await App.Current.MainPage.DisplayAlert("", "החנות נוספה בהצלחה", "אישור", FlowDirection.RightToLeft);
                        else
                            await App.Current.MainPage.DisplayAlert("", "החנות נוספה, אך לא ניתן היה להוסיף תמונה, נא להוסיף תמונה", "אישור", FlowDirection.RightToLeft);
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
                Thread.Sleep(500);

                string originalShopManagerEmail = "";
                int shopManagerId = 0;
                if (Shop.ShopManagerId != null)
                {
                    shopManagerId = (int)Shop.ShopManagerId;
                    User user = await proxy.GetUserAsync(shopManagerId);

                    if (user != null)
                        originalShopManagerEmail = user.UserEmail;
                }

                if (originalShopManagerEmail != ShopManagerEmail)
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
                if (isUpdatedShop && this.imageFileResult != null)
                {
                    ServerStatus = "מעלה תמונה...";

                    successImg = await proxy.UploadShopImage(new FileInfo()
                    {
                        Name = this.imageFileResult.FullPath
                    }, $"{Shop.ShopId}.png");
                }
                await App.Current.MainPage.Navigation.PopModalAsync();

                if (isUpdatedShop)
                {
                    if ((Shop.ShopManagerId != null) && (originalShopManagerEmail != shopManagerEmail))
                        await proxy.DeleteShopManager((int)Shop.ShopManagerId);
                    if(successImg)
                        await App.Current.MainPage.DisplayAlert("החנות התעדכנה בהצלחה", "", "אישור", FlowDirection.RightToLeft);
                    else
                        await App.Current.MainPage.DisplayAlert("החנות התעדכנה אך לא ניתן היה לשנות את התמונה", "", "אישור", FlowDirection.RightToLeft);
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
