using System.ComponentModel;
using Xamarin.Forms;
using HelloXamarin.ViewModels;

namespace HelloXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}