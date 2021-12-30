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
    public partial class ServerStatus : ContentPage
    {
        public ServerStatus(Object bc)
        {
            this.BindingContext = bc;
            InitializeComponent();
        }
    }
}