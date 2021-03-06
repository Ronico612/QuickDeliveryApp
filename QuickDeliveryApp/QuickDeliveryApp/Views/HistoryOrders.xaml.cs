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
    public partial class HistoryOrders : ContentPage
    {
        public HistoryOrders()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HistoryOrdersViewModel context = (HistoryOrdersViewModel)this.BindingContext;
            context.InitOrders();
        }
    }
}