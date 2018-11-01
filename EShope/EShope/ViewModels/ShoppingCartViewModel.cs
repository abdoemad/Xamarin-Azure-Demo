using EShope.Models;
using EShope.Services.Data;
using EShope.Services.Infra;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EShope.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        #region Private Variables

        #endregion

        public ShoppingCartViewModel(IOrderService orderService, IMapper mapper)
        {
            _cartList = new ObservableCollection<CartItemViewModel>();
            CartList.CollectionChanged += CartList_CollectionChanged;
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
        public void DeleteCartItem(CartItemViewModel cartItem)
        {
            _cartList.Remove(cartItem);
            CartListChanged(this, null);
            RaisePropertyChanged(() => CartList);
        }

        public void AddProductToCartList(Product product, int quantities)
        {
            CartItemViewModel cartItem = _cartList.FirstOrDefault(c => c.Product.Id == product.Id);
            if (cartItem == null)
            {
                cartItem = new CartItemViewModel(product, quantities);
                _cartList.Add(cartItem);
                RaisePropertyChanged(() => CartList);
            }
            else
                cartItem.AddQuantities(quantities);

            CartListChanged(this, null);
        }

        public void DecrementCartItemQuantity(Product product)
        {
            CartItemViewModel cartItem = _cartList.FirstOrDefault(c => c.Product.Id == product.Id);
            if (cartItem == null)
                throw new Exception($"Trying to decrement the quantity of product '{product.Name}' witch doesn't exist yet in the cart!" +
                    $"{Environment.NewLine}Cart Items ({_cartList.Count}): {string.Join(", ", _cartList.Select(c => c.Product.Name))}");

            cartItem.DecrementQuantity();

            CartListChanged(this, null);
        } 
        #endregion

        #region Private Methods
        private void CheckoutOrder()
        {

        }

        private void CartList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => TotalQuantities);
        }
        #endregion
    }
}
