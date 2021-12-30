using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuickDeliveryApp.Views;
using QuickDeliveryApp.Models;

namespace QuickDeliveryApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv 
        { 
            get
            {
                return true;
            }
        }

        public User CurrentUser
        {
            get; set;
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TheMainTabbedPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
