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
        //protected NavigationPage _rootPage;
        public NavigationService()
        {
            CreatePageViewModelMappings();
            //_rootPage
        }

        private void CreatePageViewModelMappings()
        {
            _homePageType = typeof(HomePage);
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
            await viewModel.Initialize();
        }

        public async Task NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var pageType = _mappings[typeof(TViewModel)];
            var page = Activator.CreateInstance(pageType) as PageBase;
            NavigationPage navigationPage = App.AppMainPage as NavigationPage;
            if (navigationPage == null)
                await Task.FromResult(false);
            await navigationPage.PushAsync(page);
            var viewModel = page.BindingContext as ViewModelBase;
            await viewModel.Initialize();
        }
    }
}
