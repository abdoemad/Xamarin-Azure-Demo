using EShope.Models;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
        CartItemViewModel _cartItem;
        ShoppingCartViewModel _shoppingCartViewModel;
        public ProductDetailsViewModel(ProductViewModel product) 
        {
            _shoppingCartViewModel = ViewModelLocator.Resolve<ShoppingCartViewModel>();

            var cart = _shoppingCartViewModel.GetCartItemByProduct(product.Id);
            if (cart == null)
                _cartItem = new CartItemViewModel(product, 1);
        }
        public CartItemViewModel CartItem => _cartItem;
        public int Quantity { get; set; }
        
    }
}
