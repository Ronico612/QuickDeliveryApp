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
    public partial class AddOrEditShop : ContentPage
    {
        public AddOrEditShop()
        {
            InitializeComponent();
        }

        public void SetImageSourceEvent(ImageSource obj)
        {
            theImage.Source = obj;
        }
    }
}