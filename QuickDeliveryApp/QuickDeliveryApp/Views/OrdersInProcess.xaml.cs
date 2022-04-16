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
    public partial class OrdersInProcess : ContentPage
    {
        public OrdersInProcess()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OrdersInProcessViewModel context = (OrdersInProcessViewModel)this.BindingContext;
            context.InitOrders();
        }
    }
}