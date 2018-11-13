using EShope.Models;
using EShope.Services.UI;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EShope.ViewModels
{
    public class AddToCartViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        ShoppingCartViewModel _shoppingCartViewModel;
        ProductViewModel _product;
        public AddToCartViewModel(ProductViewModel product)
        {
            _product = product;
            _quantity = 1;

            _navigationService = ViewModelLocator.Resolve<INavigationService>();
            _shoppingCartViewModel = ViewModelLocator.Resolve<ShoppingCartViewModel>();
            
        }
        int _quantity;
        public int Quantity { get => _quantity; set => SetProperty(ref _quantity, value); }
        public ProductViewModel Product => _product;

        private readonly ICommand _addToCartCommand;
        public ICommand AddToCartCommand => _addToCartCommand ?? new Command(async () =>
        {
            //var cartItem = _shoppingCartViewModel.GetCartItemByProduct(Product.Id);
            //if (IsNewCartItem)
            _shoppingCartViewModel.AddProductToCartList(_product, _quantity);
            //else
            //{
            //    //cartItem.AddQuantities(_cartItem.Quantity);
            //    _shoppingCartViewModel.UpdateCartItemQuantities(_cartItem);//, _cartItem.Quantity
            //}
            await _navigationService.NavigateBackAsync();
        });

    }
}
