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
    public partial class HistoryDelivery : ContentPage
    {
        public HistoryDelivery()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HistoryDeliveryViewModel context = (HistoryDeliveryViewModel)this.BindingContext;
            context.InitOrders();
        }
    }
}