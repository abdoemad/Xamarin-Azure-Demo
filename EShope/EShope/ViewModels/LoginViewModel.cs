using EShope.Models;
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


        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            User = new User() { };

            User.Errors.ErrorsChanged += Errors_ErrorsChanged;
        }

        private void Errors_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            LoginCommand.RaiseCanExecuteChanged();
        }

        #region Properties
        User _user;
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        #endregion

        public RelayCommand LoginCommand => new RelayCommand(() =>
        {
            User.ValidateProperties();
            if (User.HasErrors)
                return;

            _navigationService.NagigatoToHomePage();

        }, () => !User.HasErrors);
    }
}
