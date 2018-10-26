using EShope.Models;
using EShope.Services.Infra;
using EShope.Services.UI;
using EShope.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        IAuthenticationService _authenticationService;

        public LoginViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;

            User = new UserViewMode() { };

            User.Errors.ErrorsChanged += Errors_ErrorsChanged;
        }

        private void Errors_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            LoginCommand.RaiseCanExecuteChanged();
        }

        #region Properties
        UserViewMode _user;
        public UserViewMode User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        #endregion

        public RelayCommand LoginCommand => new RelayCommand(async () =>
        {
            User.ValidateProperties();
            if (User.HasErrors)
                return;
            IsBusy = true;
            var authnticationResponse = await _authenticationService.Authenticate(User.UserName, string.Empty); 
            IsBusy = false;
            if (authnticationResponse.IsAuthenticated)
                await _navigationService.NagigatoToHomePage();

        }, () => !User.HasErrors);
    }
}
