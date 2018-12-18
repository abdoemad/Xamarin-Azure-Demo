using EShope.Common;
using EShope.Services.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels.Base
{
    public class ShoppingViewModelBase : ViewModelBase
    {
        IDialogService _dialogService;
        INavigationService _navigationService;
        public ShoppingViewModelBase()
        {
            _dialogService = ViewModelLocator.Resolve<IDialogService>();
            _navigationService = ViewModelLocator.Resolve<INavigationService>();
        }
        IAsyncCommand _logoutCommand;

        public IAsyncCommand LogoutCommand => _logoutCommand ?? new AsyncCommand(async () =>
         {
             App.LoggedInUser = null;
             await Task.Delay(150);
             _dialogService.HideMenu();
             await _navigationService.NavigateToLoginPage();
         });

        IAsyncCommand _goToHomeCommand;

        public IAsyncCommand GoToHomeCommand => _goToHomeCommand ?? new AsyncCommand(async () =>
        {
            _dialogService.HideMenu();
            await _navigationService.NagigatoToHomePage();
        });

        private readonly ICommand _requestSyncCommand;

        public ICommand RequestSyncCommand => _requestSyncCommand ?? new Command(() =>
       {
           MessagingCenter.Send<object>(this, "Sync request");

           _dialogService.HideMenu();
       });
    }
}
