using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        public ProductDetailsViewModel()
        {
        }
        public int Quantity { get; set; }
    }
}
