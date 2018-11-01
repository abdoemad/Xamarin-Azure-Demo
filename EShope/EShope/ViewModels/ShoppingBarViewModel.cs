using EShope.Services.Device;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.ViewModels
{
    public class ShoppingBarViewModel : BaseViewModel, IDisposable
    {
        
        private readonly IConnectionService _connectionService;
        private ShoppingCartViewModel _shoppingCartViewModel;
        public ShoppingBarViewModel(IConnectionService connectionService, ShoppingCartViewModel shoppingCartViewModel)
        {
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
    }
}
