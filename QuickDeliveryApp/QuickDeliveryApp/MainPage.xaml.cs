using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using QuickDeliveryApp.Services;

namespace QuickDeliveryApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btn_Clicked(object sender, EventArgs e)
        {
            QuickDeliveryAPIProxy proxy = QuickDeliveryAPIProxy.CreateProxy();
            lbl.Text = await proxy.TestAsync(); 
        }
    }
}
