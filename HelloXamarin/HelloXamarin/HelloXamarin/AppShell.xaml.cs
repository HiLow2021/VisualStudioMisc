using System;
using System.Collections.Generic;
using HelloXamarin.ViewModels;
using HelloXamarin.Views;
using Xamarin.Forms;

namespace HelloXamarin
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
