using EShope.Common;
using EShope.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Models
{
    public class ProductViewModel : ObserverBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumnailURL { get; set; }

        public string[] PicturesURLs { get; set; }

        public decimal Price { get; set; }
        private int _availableQuantity;
        public int AvailableQuantity { get { return _availableQuantity; } private set { _availableQuantity = value; } }
        public string ShortDescription => Description.TruncateLongString(70);

        public void DeductAvailableQuantity(int quantities)
        {
            _availableQuantity -= quantities;
            RaisePropertyChanged(() => AvailableQuantity);
        }

        public void RestoreQuantities(int quantities)
        {
            _availableQuantity += quantities;
            RaisePropertyChanged(() => AvailableQuantity);
        }
    }
}
