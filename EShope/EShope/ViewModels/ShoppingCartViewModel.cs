using EShope.Helpers;
using EShope.Models;
using EShope.Services.Data;
using EShope.Services.Data.Models;
using EShope.Services.Device;
using EShope.Services.Infra;
using EShope.Services.UI;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        #region Private Variables
        IOrderService _orderService;
        IDialogService _dialogService;
        IConnectionService _connectionService;
        INavigationService _navigationService;
        #endregion

        public ShoppingCartViewModel(IOrderService orderService, IMapper mapper, IDialogService dialogService, IConnectionService connectionService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _connectionService = connectionService;
            _dialogService = dialogService;
            _orderService = orderService;
            _cartList = new ObservableCollection<CartItemViewModel>();
            CartList.CollectionChanged += CartList_CollectionChanged;

            var mapConfig = new Dictionary<Type, Type>
            {
                {typeof(CartItemViewModel), typeof(OrderItem) }
            };
            //mapper.Initialize(mapConfig);
        }

        #region Public Variables
        public event EventHandler CartListChanged;
        #endregion

        #region Public Properties
        private readonly ObservableCollection<CartItemViewModel> _cartList;
        public ObservableCollection<CartItemViewModel> CartList => _cartList;
        public int TotalQuantities => _cartList.Sum(c => c.Quantity);

        #endregion

        #region Public Methods

        public CartItemViewModel GetCartItemByProduct(string productId)
        {
            return _cartList.FirstOrDefault(cart => cart.Product.Id == productId);
        }
        //public void DeleteCartItem(CartItemViewModel cartItem)
        //{
        //    _cartList.Remove(cartItem);
        //    CartListChanged(this, null);
        //    RaisePropertyChanged(() => CartList);
        //}

        public void AddProductToCartList(ProductViewModel product, int quantities)
        {
            CartItemViewModel cartItem = _cartList.FirstOrDefault(c => c.Product.Id == product.Id);
            if (cartItem == null)
            {
                cartItem = new CartItemViewModel(product, quantities);
                _cartList.Add(cartItem);
                RaisePropertyChanged(() => CartList);
            }
            else
            {
                cartItem.AddQuantities(quantities);
            }
            product.DeductQuantities(quantities);
            CartListChanged?.Invoke(this, null);
        }

        //public void UpdateCartItemQuantities(CartItemViewModel cartItem)//, int quantities
        //{
        //    var existCartItem = _cartList.FirstOrDefault(c => c.Product.Id == cartItem.Product.Id);
        //    var delta = (cartItem.Quantity + 1) - existCartItem.Quantity;
        //    if (delta >= 1)
        //    {
        //        existCartItem.AddQuantities(delta);
        //    }
        //    else
        //    {
        //        existCartItem.SubtractQuantiries((delta * -1) + 1);
        //    }

        //    CartListChanged?.Invoke(this, null);
        //}
        //public void AddCartItemToCartList(CartItemViewModel cartItem)
        //{
        //    ExceptionHelper.TryCatch(() =>
        //    {
        //        var cart = _cartList.FirstOrDefault(c => c.Product.Id == cartItem.Product.Id);
        //        if (cart == null)
        //        {
        //            cart = cartItem;
        //            _cartList.Add(cart);
        //        }
        //        else
        //        {
        //            cart.AddQuantities(cartItem.Quantity);
        //        }
        //        cart.Product.DeductQuantities(cartItem.Quantity);
        //        CartListChanged?.Invoke(this, null);
        //    });
        //}

        //public void DecrementCartItemQuantity(ProductViewModel product)
        //{
        //    CartItemViewModel cartItem = _cartList.FirstOrDefault(c => c.Product.Id == product.Id);
        //    if (cartItem == null)
        //        throw new Exception($"Trying to decrement the quantity of product '{product.Name}' witch doesn't exist yet in the cart!" +
        //            $"{Environment.NewLine}Cart Items ({_cartList.Count}): {string.Join(", ", _cartList.Select(c => c.Product.Name))}");

        //    cartItem.DecrementQuantity();

        //    CartListChanged?.Invoke(this, null);
        //}
        #endregion

        #region Private Methods

        private void CartList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => TotalQuantities);
        }
        #endregion

        private ICommand _deleteCartItemCommand;

        public ICommand DeleteCartItemCommand => _deleteCartItemCommand ?? new Command<CartItemViewModel>((cartItem) =>
        {
            _cartList.Remove(cartItem);
            cartItem.Product.RestoreQuantities(cartItem.Quantity);
            CartListChanged?.Invoke(this, null);
        });

        private ICommand _submitCommand;

        public ICommand SubmitCommand => _submitCommand ?? new Command(async () =>
        {
            if (!_connectionService.IsConnected)
            {
                await _dialogService.ShowDialog("Warning", "Please turn on internet connectivity in order to submit your order", "OK");
                return;
            }

            if (App.LoggedInUser.Id == null || App.LoggedInUser.Id == Guid.Empty)
            {
                await _dialogService.ShowDialog("Warning", $"The Signed In user '{App.LoggedInUser.UserName}' is not online verified", "OK");
                return;
            }
            IsBusy = true;
            var order = new Order
            {
                UserId = App.LoggedInUser.Id,
                OrderItems = _cartList.Select(c => new OrderItem
                {
                    ProductId = c.Product.Id.ToString(),
                    Quantity = c.Quantity
                }).ToList()
            };

            var orderId = await _orderService.CheckoutAsync(order);
            if (!string.IsNullOrEmpty(orderId) && orderId != Guid.Empty.ToString())
            {
                _cartList.Clear();
                CartListChanged?.Invoke(this, null);
                await _navigationService.ClearStack();
            }
            IsBusy = false;
        });
    }
}
