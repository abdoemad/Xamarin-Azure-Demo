using EShope.Helpers;
using EShope.Pages;
using EShope.Pages.Base;
using EShope.ViewModels;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EShope.Services.UI.Imp
{
    public class NavigationService : INavigationService
    {
        private Dictionary<Type, Type> _mappings;

        protected Type _homePageType;
        protected Type _loginPageType;
        protected Type _navigationRootPage;
        public NavigationService()
        {
            CreatePageViewModelMappings();
        }
        protected Page MainPage => Application.Current.MainPage;
        private void CreatePageViewModelMappings()
        {
            _homePageType = typeof(HomePage);
            _loginPageType = typeof(LoginPage);
            _navigationRootPage = typeof(MainPage);

            _mappings = new Dictionary<Type, Type>
            {
                { typeof(ProductCatalogViewModel), typeof(HomePage) },
                { typeof(ProductDetailsViewModel), typeof(ProductDetailsPage) },
                { typeof(ShoppingCartViewModel), typeof(ShoppingCartPage) },
                { typeof(LoginViewModel), typeof(LoginPage) }
            };
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public async Task NagigatoToHomePage()
        {
            PageBase homePage = Activator.CreateInstance(_homePageType) as PageBase;
            Application.Current.MainPage = new MainPage(homePage);
            var viewModel = homePage.BindingContext as ViewModelBase;
            await viewModel.InitializeAsync(null);
        }

        public async Task NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var pageType = _mappings[typeof(TViewModel)];
            var page = Activator.CreateInstance(pageType) as PageBase;
            NavigationPage navigationPage = App.AppMainPage as MainPage;
            if (navigationPage == null)
                await Task.FromResult(false);
            await navigationPage.PushAsync(page);
            var viewModel = page.BindingContext as ViewModelBase;
            await viewModel.InitializeAsync(null);
        }

        public async Task NavigateTo<TViewModel>(object parameter, bool initiateViewModel) where TViewModel : ViewModelBase
        {
            await ExceptionHandlingHelper.TryCatchAsync(async () =>
            {
                var navigationPage = App.AppMainPage as MainPage;
                if (navigationPage == null)
                    await Task.FromResult(false);

                PageBase page = null;
                ViewModelBase viewModel;
                var pageType = _mappings[typeof(TViewModel)];

                //if (initiateViewModel)
                //{
                //    viewModel = Activator.CreateInstance(typeof(TViewModel), parameter) as ViewModelBase;
                //page = Activator.CreateInstance(pageType, viewModel) as PageBase;
                //}
                //else
                page = Activator.CreateInstance(pageType) as PageBase;
                if (initiateViewModel)
                {
                    page.BindingContext = Activator.CreateInstance(typeof(TViewModel), parameter);
                }
                await navigationPage.PushAsync(page);

                viewModel = page.BindingContext as ViewModelBase;
                await viewModel.InitializeAsync(parameter);
            });
        }

        public async Task NavigateBackAsync()
        {
            if (MainPage is MainPage mainPage)
            {
                await mainPage.Navigation.PopAsync();
            }
        }
    }
}
