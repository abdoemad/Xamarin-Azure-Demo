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

            PropertyChanged += CartItemViewModel_PropertyChanged;
        }

        private void CartItemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Quantity))
                QuantityChanged?.Invoke(this, null);
        }
        public event EventHandler QuantityChanged;
        public ProductViewModel Product => _product;
        public int ProductAvailableQuantity => _product.AvailableQuantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            //set
            //{
            //    SetProperty(ref _quantity, value);
            //}
        }
        public void AddQuantities(int quantities)
        {
            _quantity += quantities;
            //_product.DeductQuantities(quantities);
            RaisePropertyChanged(() => Quantity);
        }
        public void SubtractQuantiries(int quantities)
        {
            _quantity -= quantities;
            //_product.RestoreQuantities(quantities);
            RaisePropertyChanged(() => Quantity);
        }
        public void IncrementQuantity() {
            _quantity++;
            //_product.DeductQuantities(1);
            RaisePropertyChanged(() => Quantity);
        }

        public void DecrementQuantity()
        {
            _quantity--;
            //_product.RestoreQuantities(1);
            RaisePropertyChanged(() => Quantity);
        }

    }
}
