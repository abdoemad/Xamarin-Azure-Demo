using EShope.Models;
using EShope.Pages;
using EShope.ViewModels;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EShope
{
    public partial class App : Application
    {
        public static Page AppMainPage => Current.MainPage;

        public static IDictionary<string, object> AppProperties => Current.Properties;

        public static INavigation Navigation => Current.MainPage.Navigation;

        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public static UserViewMode LoggedInUser { get; set; }
        public App()
        {
            InitializeComponent();

            InitializeApp();

            MainPage = new LoginPage(); // new MainPage(new HomePage()); //
        }

        private void InitializeApp()
        {
            //DependencyResolver.ResolveUsing(type=>_locator.

            //CachedImage.FixedOnMeasureBehavior = true;
            //CachedImage.FixedAndroidMotionEventHandler = true;
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
