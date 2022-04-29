using QuickDeliveryApp.Services;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickDeliveryApp.ViewModels
{
    class AddDeliveryPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region DeliveryPersonEmail
        private string deliveryPersonEmail;
        public string DeliveryPersonEmail
        {
            get { return deliveryPersonEmail; }
            set
            {
                deliveryPersonEmail = value;
                OnPropertyChanged("DeliveryPersonEmail");
            }
        }

        private bool showDeliveryPersonEmailError;
        public bool ShowDeliveryPersonEmailError
        {
            get => showDeliveryPersonEmailError;
            set
            {
                showDeliveryPersonEmailError = value;
                OnPropertyChanged("ShowDeliveryPersonEmailError");
            }
        }

        private string deliveryPersonEmailError;
        public string DeliveryPersonEmailError
        {
            get => deliveryPersonEmailError;
            set
            {
                deliveryPersonEmailError = value;
                OnPropertyChanged("DeliveryPersonEmailError");
            }
        }

        private void ValidateDeliveryPersonEmail()
        {
            if (DeliveryPersonEmail == null)
                this.ShowDeliveryPersonEmailError = true;
            else
                this.ShowDeliveryPersonEmailError = string.IsNullOrEmpty(DeliveryPersonEmail.Trim());

            if (!this.ShowDeliveryPersonEmailError)
            {
                if (!Regex.IsMatch(DeliveryPersonEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowDeliveryPersonEmailError = true;
                    this.DeliveryPersonEmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.DeliveryPersonEmailError = ERROR_MESSAGES.REQUIRED_FIELD;
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

        public AddDeliveryPersonViewModel()
        {
            this.DeliveryPersonEmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }

        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateDeliveryPersonEmail();

            //Check if any validation failed
            if (showDeliveryPersonEmailError)
                return false;
            return true;
        }

        public ICommand AddCommand => new Command(Add);
        public async void Add()
        {
            if (!ValidateForm())
                return;

            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            bool isEmailExist = await proxy.IsUserEmailExistAsync(DeliveryPersonEmail);
            if (!isEmailExist)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "שליח אינו רשום במערכת", "בסדר", FlowDirection.RightToLeft);
                return;
            }
            ServerStatus = "מוסיף שליח...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatus(this));

            Thread.Sleep(500);
          
            int dpId = await proxy.AddDeliveryPersonAsync(DeliveryPersonEmail);
            await App.Current.MainPage.Navigation.PopModalAsync();
            if (dpId == -1)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת השליח נכשלה. בדקו אם השליח כבר קיים במערכת", "בסדר", FlowDirection.RightToLeft);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "השליח נוסף בהצלחה", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
