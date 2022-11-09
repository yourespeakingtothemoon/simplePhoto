using simplePhoto.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace simplePhoto.Views
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