using EShope.Models;
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
    public class ProductDetailsViewModel : ViewModelBase
    {
        CartItemViewModel _cartItem;
        ShoppingCartViewModel _shoppingCartViewModel;
        INavigationService _navigationService;
        public ProductDetailsViewModel(ProductViewModel product)
        {
            _navigationService = ViewModelLocator.Resolve<INavigationService>();
            _shoppingCartViewModel = ViewModelLocator.Resolve<ShoppingCartViewModel>();

            var cart = _shoppingCartViewModel.GetCartItemByProduct(product.Id);
            if (cart == null)
            {
                cart = new CartItemViewModel(product, 1);
                IsNewCartItem = true;
            }
            _cartItem = cart;
            //_quantity = _cartItem.Quantity;
        }
        public CartItemViewModel CartItem => _cartItem;
        public bool IsNewCartItem { get; set; } = false;
        public ProductViewModel Product => _cartItem.Product;
        //public string ProductName => _cartItem.Product.Name;
        //int _quantity;
        //public int Quantity { get => _quantity; set => SetProperty(ref _quantity, value); }
        public int ProductAvailableQuantity => _cartItem.Product.AvailableQuantity;
        private ICommand _quantityChangedCommand;

        public ICommand QuantityChangedCommand => _quantityChangedCommand ?? new Command((quantity) =>
        {
            var intQuantity = Convert.ToDouble(quantity);
            if (_cartItem.Quantity < intQuantity)
            {
                _cartItem.IncrementQuantity();
            }
            else if (_cartItem.Quantity > intQuantity)
            {
                _cartItem.DecrementQuantity();
            }
        });

        private readonly ICommand _addToCartCommand;
        public ICommand AddToCartCommand => _addToCartCommand ?? new Command(async () =>
        {
            var cartItem = _shoppingCartViewModel.GetCartItemByProduct(Product.Id);
            if (cartItem == null)
                _shoppingCartViewModel.AddCartItemToCartList(_cartItem);
            else
            {
                //cartItem.AddQuantities(_cartItem.Quantity);
                _shoppingCartViewModel.UpdateCartItemQuantities(cartItem, _cartItem.Quantity);
            }
            await _navigationService.NavigateBackAsync();
        });
    }
}
