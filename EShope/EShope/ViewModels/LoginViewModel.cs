using EShope.Models;
using EShope.Resources;
using EShope.Services.Device;
using EShope.Services.Infra;
using EShope.Services.UI;
using EShope.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IConnectionService _connectionService;
        public LoginViewModel(INavigationService navigationService,IDialogService dialogService, IAuthenticationService authenticationService, IConnectionService connectionService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _authenticationService = authenticationService;
            _connectionService = connectionService;

            User = new UserViewMode() { };

            User.PropertyChanged += User_PropertyChanged;
            //User.Errors.ErrorsChanged += Errors_ErrorsChanged;

            RaisePropertyChanged(() => IsUserEntityValid);
        }

        private void User_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(User.UserName))
            {
                RaisePropertyChanged(() => IsUserEntityValid);
            }
        }

        private void Errors_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            //LoginCommand.RaiseCanExecuteChanged();
        }

        #region Properties
        UserViewMode _user;
        public UserViewMode User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        #endregion
        private readonly ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ?? new Command(async () =>//
        {
            //Device.BeginInvokeOnMainThread(() => 
            //{
                IsBusy = true;
            //});
              //var res = await User.ValidatePropertiesAsync();
              //if (User.HasErrors)
              //{
              //    IsBusy = false;
              //    RaisePropertyChanged(() => IsUserEntityValid);
              //    return;
              //}

            if (_connectionService.IsConnected)
            {
                try
                {
                    var authnticationResponse = await _authenticationService.Authenticate(User.UserName, string.Empty);

                    if (authnticationResponse.IsAuthenticated)
                    {
                        User.Id = authnticationResponse.User.Id;
                        //User.IsOnlineAuthenticate = true;
                    }
                }
                catch (Exception ex)
                {
                    //IsBusy = false;
                    await _dialogService.ShowDialog("Warning", ErrorMessagesResources.Login_ServerAuthenticationFailed,  "Ok");
                    //return;
                }
            }
            App.LoggedInUser = User;
            await _navigationService.NagigatoToHomePage();
            IsBusy = false;

        }, () => true/*!User.HasErrors*/);

        public bool IsUserEntityValid => true; // !User.HasErrors;
    }
}
