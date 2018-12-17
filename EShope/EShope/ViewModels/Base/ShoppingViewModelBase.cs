using EShope.Common;
using EShope.Services.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.ViewModels.Base
{
    public class ShoppingViewModelBase : ViewModelBase
    {
        IDialogService _dialogService;
        INavigationService _navigationService;
        public ShoppingViewModelBase() {
            _dialogService = ViewModelLocator.Resolve<IDialogService>();
            _navigationService = ViewModelLocator.Resolve<INavigationService>();
        }
        IAsyncCommand _logoutCommand;

        public IAsyncCommand LogoutCommand => _logoutCommand ?? new AsyncCommand(async () =>
         {
             App.LoggedInUser = null;
             await Task.Delay(150);
             await _navigationService.NavigateToLoginPage();
         });

        IAsyncCommand _goToHomeCommand;

        public IAsyncCommand GoToHomeCommand => _goToHomeCommand ?? new AsyncCommand(async () =>
        {
            await _navigationService.NagigatoToHomePage();
        });
    }
}
