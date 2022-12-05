using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicturePickle
{
    public partial class App : Application
    {

        public static MainPage main = new MainPage();
        public App()
        {
            InitializeComponent();

            MainPage = main;
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
