using EShope.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EShope
{
    public partial class App : Application
    {
        public static Page AppMainPage => Current.MainPage;

        public static IDictionary<string, object> AppProperties => Current.Properties;

        public App()
        {
            InitializeComponent();
            
            MainPage = new LoginPage();
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
