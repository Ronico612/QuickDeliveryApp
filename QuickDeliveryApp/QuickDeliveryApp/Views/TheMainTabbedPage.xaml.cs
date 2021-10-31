﻿using QuickDeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QuickDeliveryApp.Services;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheMainTabbedPage : TabbedPage
    {
        public Shops shopsPage;
        public ShoppingCart ShoppingCart;

        public TheMainTabbedPage()
        {
            this.BindingContext = new TheMainTabbedPageViewModel();
            InitializeComponent();

            shopsPage = new Shops();
            shopsPage.Title = "Shops";
            this.Children.Add(shopsPage);

            ShoppingCart = new ShoppingCart();
            ShoppingCart.Title = "Shopping Cart";
            this.Children.Add(ShoppingCart);
        }
    }
}