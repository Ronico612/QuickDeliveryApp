using QuickDeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDeliveryPManagement : ContentPage
    {
        public AdminDeliveryPManagement()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App app = (App)Application.Current;
            AdminDeliveryPManagementViewModel context = (AdminDeliveryPManagementViewModel)this.BindingContext;
            context.InitDeliveryPersons();
        }
    }
}