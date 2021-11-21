using QuickDeliveryApp.Services;
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
    public partial class Shops : ContentPage
    {
        public Shops()
        {
            this.BindingContext = new ShopsViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ShopsViewModel context = (ShopsViewModel)this.BindingContext;
            collectionName.SelectedItem = null;

        }
    }

    
}