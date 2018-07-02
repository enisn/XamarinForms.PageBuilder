using System;
using Xamarin.Forms;
using ControlDesigner.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ControlDesigner
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MasterDetailPage
            {
                Master = new MasterPage(),
                Detail = new NavigationPage( new LandingPage())
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
