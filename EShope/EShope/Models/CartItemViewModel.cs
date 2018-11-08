using EShope.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Models
{
    public class CartItemViewModel : ObserverBase
    {
        private int _quantity;
        private ProductViewModel _product;
        public CartItemViewModel(ProductViewModel product, int quantity = 1)
        {
            _product = product;
            _quantity = quantity;
        }
        public ProductViewModel Product { get; set; }
        
        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }
        public void AddQuantities(int quantities)
        {
            _quantity += quantities;
            RaisePropertyChanged(() => Quantity);
        }
        public void IncrementQuantity() {
            _quantity++;
            RaisePropertyChanged(() => Quantity);
        }

        public void DecrementQuantity()
        {
            _quantity--;
            RaisePropertyChanged(() => Quantity);
        }

    }
}
