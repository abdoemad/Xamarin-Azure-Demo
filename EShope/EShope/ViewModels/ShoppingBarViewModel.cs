using EShope.Services.Device;
using EShope.Services.UI;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels
{
    public class ShoppingBarViewModel : ViewModelBase, IDisposable
    {
        private readonly INavigationService _navigationService;
        private readonly IConnectionService _connectionService;
        private ShoppingCartViewModel _shoppingCartViewModel;
        public ShoppingBarViewModel(INavigationService navigationService, IConnectionService connectionService, ShoppingCartViewModel shoppingCartViewModel)
        {
            _navigationService = navigationService;
            _shoppingCartViewModel = shoppingCartViewModel;
            _shoppingCartViewModel.CartListChanged += _shoppingCartViewModel_CartListChanged;

            _connectionService = connectionService;
            connectionService.ConnectivityChanged += ConnectivityChanged;
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

        public string UserName => App.LoggedInUser.UserName;

        public int CartItemsQuantities => _shoppingCartViewModel.TotalQuantities;

        public bool IsOnline => _connectionService.IsConnected;

        #region Commands
        public ICommand NavigateToShoppingCartCommand => new Command(async () =>
        {
            await _navigationService.NavigateTo<ShoppingCartViewModel>();
        });
        #endregion
    }
}
