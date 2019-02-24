using EShope.Common;
using EShope.Services.Device;
using EShope.Services.UI;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels
{
    public class ShoppingBarViewModel : ViewModelBase, IDisposable
    {
        private readonly INavigationService _navigationService;
        private readonly IConnectionService _connectionService;
        private ShoppingCartViewModel _shoppingCartViewModel;
        IDialogService _dialogService;
        public ShoppingBarViewModel(INavigationService navigationService, IConnectionService connectionService, ShoppingCartViewModel shoppingCartViewModel, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _shoppingCartViewModel = shoppingCartViewModel;
            _shoppingCartViewModel.CartListChanged += _shoppingCartViewModel_CartListChanged;

            _connectionService = connectionService;
            connectionService.ConnectivityChanged += ConnectivityChanged;

            MessagingCenter.Subscribe<object>(this, "DeviceOrientationChanged", (orientation) =>
            {
                if ((DeviceOrientations)orientation == DeviceOrientations.Landscape)
                    _userName += "*";
                else
                    _userName = _userName.TrimEnd(new char[] { '*' });
                RaisePropertyChanged(() => UserName);
            });
        }

        private void _shoppingCartViewModel_CartListChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => CartItemsQuantities);
        }

        private void ConnectivityChanged(object sender, bool e)
        {
            RaisePropertyChanged(() => IsOnline);
        }

        public void Dispose()
        {
            _connectionService.ConnectivityChanged -= ConnectivityChanged;
        }
        private string _userName;
        public string UserName => _userName = _userName ?? App.LoggedInUser.UserName;

        public int CartItemsQuantities => _shoppingCartViewModel.TotalQuantities;

        public bool IsOnline => _connectionService.IsConnected;

        #region Commands
        public IAsyncCommand NavigateToShoppingCartCommand => new AsyncCommand(async () =>
        {
            await _navigationService.NavigateTo<ShoppingCartViewModel>();
        });

        private IAsyncCommand _openMenuCommand;
        public IAsyncCommand OpenMenuCommand => _openMenuCommand ?? new AsyncCommand(async () =>
        {
            await Task.Delay(200);
            _dialogService.ToggleMenuVisibility();
        });
        #endregion
    }
}
