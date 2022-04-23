using QuickDeliveryApp.Models;
using QuickDeliveryApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class AddOrEditProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Product product;
        public Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product != value)
                {

                    this.product = value;
                    OnPropertyChanged("Product");
                }
            }
        }

        private bool isAddded;
        public bool IsAdded
        {
            get
            {
                return this.isAddded;
            }
            set
            {
                if (this.isAddded != value)
                {

                    this.isAddded = value;
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

        #region ProductName
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        private bool showProductNameError;
        public bool ShowProductNameError
        {
            get => showProductNameError;
            set
            {
                showProductNameError = value;
                OnPropertyChanged("ShowProductNameError");
            }
        }

        private string productNameError;
        public string ProductNameError
        {
            get => productNameError;
            set
            {
                productNameError = value;
                OnPropertyChanged("ProductNameError");
            }
        }

        private void ValidateProductName()
        {
            if (ProductName == null)
                this.ShowProductNameError = true;
            else
                this.ShowProductNameError = string.IsNullOrEmpty(ProductName.Trim());

            if (!ShowProductNameError)
            {
                // בדיקה אם שם המוצר קיים
                App app = (App)Application.Current;
                Shop currentShop = app.AllShops.Where(s => s.ShopManagerId == app.CurrentUser.UserId).FirstOrDefault();
                Product product = currentShop.Products.Where(p => p.ProductName == ProductName).FirstOrDefault();
                if ((((IsAdded) && (product != null)) || ((!IsAdded) && (product != null) && (product.ProductId != Product.ProductId))))
                {
                    this.ShowProductNameError = true;
                    this.ProductNameError = ERROR_MESSAGES.PRODUCT_EXIST;
                }
            }
            else
                this.ProductNameError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Count
        private string count;
        public string Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        private bool showCountError;
        public bool ShowCountError
        {
            get => showCountError;
            set
            {
                showCountError = value;
                OnPropertyChanged("ShowCountError");
            }
        }

        private string countError;
        public string CountError
        {
            get => countError;
            set
            {
                countError = value;
                OnPropertyChanged("CountError");
            }
        }

        private void ValidateCount()
        {
            if (Count == null)
                this.ShowCountError = true;
            else
                this.ShowCountError = string.IsNullOrEmpty(Count.Trim());
            if (!this.ShowCountError)
            {
                if (!Regex.IsMatch(this.Count, @"^[0-9]*$"))
                {
                    this.ShowCountError = true;
                    this.CountError = ERROR_MESSAGES.BAD_NUMBER;
                }
            }
            else
                this.CountError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Price
        private string price;
        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        private bool showPriceError;
        public bool ShowPriceError
        {
            get => showPriceError;
            set
            {
                showPriceError = value;
                OnPropertyChanged("ShowPriceError");
            }
        }

        private string priceError;
        public string PriceError
        {
            get => priceError;
            set
            {
                priceError = value;
                OnPropertyChanged("PriceError");
            }
        }

        private void ValidatePrice()
        {
            if (Price == null)
                this.ShowPriceError = true;
            else
                this.ShowPriceError = string.IsNullOrEmpty(Price.Trim());
            if (!this.ShowPriceError)
            {
                if (!Decimal.TryParse(this.Price, out decimal p))
                {
                    this.ShowPriceError = true;
                    this.PriceError = ERROR_MESSAGES.BAD_NUMBER;
                }
            }
            else
                this.PriceError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region AgeProductTypes
        private List<AgeProductType> ageProductTypes;
        public List<AgeProductType> AgeProductTypes
        {
            get { return ageProductTypes; }
            set
            {
                ageProductTypes = value;
                OnPropertyChanged("AgeProductTypes");
            }
        }
        #endregion

        #region ProductTypes
        private List<ProductType> productTypes;
        public List<ProductType> ProductTypes
        {
            get { return productTypes; }
            set
            {
                productTypes = value;
                OnPropertyChanged("ProductTypes");
            }
        }
        #endregion

        #region AgeType
        private AgeProductType ageType;
        public AgeProductType AgeType
        {
            get { return ageType; }
            set
            {
                ageType = value;
                OnPropertyChanged("AgeType");
            }
        }

        private bool showAgeTypeError;
        public bool ShowAgeTypeError
        {
            get => showAgeTypeError;
            set
            {
                showAgeTypeError = value;
                OnPropertyChanged("ShowAgeTypeError");
            }
        }

        private string ageTypeError;
        public string AgeTypeError
        {
            get => ageTypeError;
            set
            {
                ageTypeError = value;
                OnPropertyChanged("AgeTypeError");
            }
        }

        private void ValidateAgeType()
        {
            if (AgeType == null)
            {
                this.ShowAgeTypeError = true;
                this.AgeTypeError = ERROR_MESSAGES.REQUIRED_FIELD;
            }

            else
                this.ShowAgeTypeError = false;   
        }

        #endregion

        #region Type
        private ProductType type;
        public ProductType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        private bool showTypeError;
        public bool ShowTypeError
        {
            get => showTypeError;
            set
            {
                showTypeError = value;
                OnPropertyChanged("ShowTypeError");
            }
        }

        private string typeError;
        public string TypeError
        {
            get => typeError;
            set
            {
                typeError = value;
                OnPropertyChanged("TypeError");
            }
        }

        private void ValidateType()
        {
            if (Type == null)
            {
                this.ShowTypeError = true;
                this.TypeError = ERROR_MESSAGES.REQUIRED_FIELD;
            }

            else
                this.ShowTypeError = false;
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

        public AddOrEditProductViewModel(Product p)
        {
            Init(p);
            this.imageFileResult = null;
            this.ProductNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CountError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PriceError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.AgeTypeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.TypeError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.ImgSourceError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        private async void Init(Product p)
        {
            await GetAgeProductTypes();
            await GetProductTypes();
            if (p == null)  // הוספת מוצר
            {
                isAddded = true;
                this.Product = new Product();
                this.ImgSource = this.Product.EmptyImgSource;
            }

            else
            {
                this.Product = p;
                this.ImgSource = this.Product.ImgSource;
                this.ProductName = Product.ProductName;
                this.Count = Product.CountProductInShop.ToString();
                this.Price = Product.ProductPrice.ToString();
                this.AgeType = this.AgeProductTypes.Where(a => a.AgeProductTypeId == Product.AgeProductType.AgeProductTypeId).FirstOrDefault();
                this.Type = this.ProductTypes.Where(t => t.ProductTypeId == Product.ProductType.ProductTypeId).FirstOrDefault();
            }
        }

        public async Task GetAgeProductTypes()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.AgeProductTypes = await quickDeliveryAPIProxy.GetAgeProductTypesAsync();
        }

        public async Task GetProductTypes()
        {
            QuickDeliveryAPIProxy quickDeliveryAPIProxy = QuickDeliveryAPIProxy.CreateProxy();
            this.ProductTypes = await quickDeliveryAPIProxy.GetProductTypesAsync();
        }

        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateProductName();
            ValidateCount();
            ValidatePrice();
            ValidateAgeType();
            ValidateType();
            ValidateImgSource();

            //Check if any validation failed
            if (ShowProductNameError || ShowCountError || ShowPriceError || ShowAgeTypeError ||
                ShowTypeError || ShowImgSourceError)
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

            bool successImg = true;
            if (IsAdded)
            {
                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();

                ServerStatus = "מוסיף מוצר...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

                Thread.Sleep(1000);
                Product newProduct = new Product();
                newProduct.ProductName = ProductName;
                if (int.TryParse(Count, out Int32 result1))
                    newProduct.CountProductInShop = result1;
                if (Decimal.TryParse(Price, out Decimal result2))
                    newProduct.ProductPrice = result2;
                newProduct.AgeProductTypeId = AgeType.AgeProductTypeId;
                newProduct.ProductTypeId = Type.ProductTypeId;
                App app = (App)Application.Current;
                newProduct.ShopId = app.AllShops.Where(s => s.ShopManagerId == app.CurrentUser.UserId).FirstOrDefault().ShopId;

                int newProductId = await proxy.AddProductAsync(newProduct);
                await App.Current.MainPage.Navigation.PopModalAsync();

                if (newProductId >= 0)
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        successImg = await proxy.UploadProductImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{newProductId}.jpg");
                    }
                    ServerStatus = "שומר נתונים...";

                    if (successImg)
                        await App.Current.MainPage.DisplayAlert("", "המוצר נוסף בהצלחה", "אישור", FlowDirection.RightToLeft);
                    else
                        await App.Current.MainPage.DisplayAlert("", "המוצר נוסף, אך לא ניתן היה להוסיף תמונה, נא להוסיף תמונה", "אישור", FlowDirection.RightToLeft);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת מוצר נכשלה", "בסדר", FlowDirection.RightToLeft);
                }
            }
            else // Update
            {
                ServerStatus = "מעדכן פרטי מוצר...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));
                Thread.Sleep(2000);

                QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
                bool isUpdatedProduct = await proxy.UpdateProduct(this.Product.ProductId, ProductName, Count, Price, AgeType.AgeProductTypeId, Type.ProductTypeId);

                if (isUpdatedProduct && this.imageFileResult != null)
                {
                    ServerStatus = "מעלה תמונה...";

                    successImg = await proxy.UploadProductImage(new FileInfo()
                    {
                        Name = this.imageFileResult.FullPath
                    }, $"{Product.ProductId}.jpg");
                }

                await App.Current.MainPage.Navigation.PopModalAsync();

                if (isUpdatedProduct)
                {
                    if (successImg)
                        await App.Current.MainPage.DisplayAlert("המוצר התעדכן בהצלחה", "", "אישור", FlowDirection.RightToLeft);
                    else
                        await App.Current.MainPage.DisplayAlert("המוצר התעדכן, אך לא ניתן היה לשנות תמונה", "", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "לא ניתן היה לעדכן פרטים", "בסדר", FlowDirection.RightToLeft);
                }
            }
        }
    }
}
