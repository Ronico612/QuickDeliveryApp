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
    public partial class ChooseOrder : ContentPage
    {
        public ChooseOrder()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();  
            ChooseOrderViewModel context = (ChooseOrderViewModel)this.BindingContext;
            context.InitOrders();
        }
    }
}