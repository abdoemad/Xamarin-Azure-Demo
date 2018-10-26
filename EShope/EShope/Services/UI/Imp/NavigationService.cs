using EShope.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EShope.Services.UI.Imp
{
    public class NavigationService : INavigationService
    {
        Type homePageType = typeof(HomePage);

        public NavigationService()
        {

        }
        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public Task NagigatoToHomePage()
        {
            var homePage = Activator.CreateInstance(homePageType) as Page;
            Application.Current.MainPage = new MainPage(homePage);
            return Task.FromResult(true);
        }

        public Task NavigateTo(Type viewModelType)
        {
            return null;
        }
    }
}
