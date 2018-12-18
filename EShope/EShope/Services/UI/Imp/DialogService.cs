using EShope.Pages.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EShope.Services.UI.Imp
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string cancelbtnLabel)
        {
            return App.AppMainPage.DisplayAlert(message, title, cancelbtnLabel);
        }

        public Task ShowDialog(string message, string title, string acceptbtnLabel, string cancelbtnLabel)
        {
            return App.AppMainPage.DisplayAlert(message, title, acceptbtnLabel, cancelbtnLabel);
        }
        public Task<IMenu> ShowMenu()
        {
            //var mainPage = App.AppMainPage;
            //mainPage
            return null;
        }

        public void ToggleMenuVisibility()
        {
            var mainPage = App.AppMainPage as MainPage;
            var shoppingBasePage = mainPage.CurrentPage as ShopppingBasePage;
            shoppingBasePage.ToggleMenuVisibility();
        }

        Task IDialogService.ShowMenu()
        {
            var mainPage = App.AppMainPage as MainPage;
            var shoppingBasePage = mainPage.CurrentPage as ShopppingBasePage;
            shoppingBasePage.IsMenuVisible = true;
            return Task.CompletedTask;
        }

        public void HideMenu()
        {
            var mainPage = App.AppMainPage as MainPage;
            if (mainPage == null)
                return;
            var shoppingBasePage = mainPage.CurrentPage as ShopppingBasePage;
            shoppingBasePage.HideMenu();
        }
    }
}
