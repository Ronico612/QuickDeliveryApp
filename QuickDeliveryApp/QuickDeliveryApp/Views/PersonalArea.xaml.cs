using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickDeliveryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalArea : ContentPage
    {
        public PersonalArea()
        {
            InitializeComponent();
            this.BindingContext = new PersonalAreaViewModel();
        }
    }
}