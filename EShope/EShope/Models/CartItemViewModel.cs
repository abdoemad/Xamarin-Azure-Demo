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
                QuantityChanged(this, null);
        }
        public event EventHandler QuantityChanged;
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
