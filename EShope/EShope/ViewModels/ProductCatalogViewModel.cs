using EShope.Models;
using EShope.Services.Data;
using EShope.Services.Data.Models;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace EShope.ViewModels
{
    public class ProductCatalogViewModel : ViewModelBase
    {
        private readonly IProductService _productService;
        private readonly Services.Infra.IMapper _mapper;
        private ObservableCollection<ProductViewModel> _productList;

        public ObservableCollection<ProductViewModel> ProductList
        {
            get => _productList;
            set
            {
                _productList = value;
                RaisePropertyChanged(() => ProductList);
            }
        }
        public ProductCatalogViewModel(IProductService productService, Services.Infra.IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

            var mapConfig = new Dictionary<Type, Type> {
                { typeof(Product), typeof(ProductViewModel) }
            };
            mapper.Initialize(mapConfig);


        }
        public override async Task OnAppearing()
        {
            IsBusy = true;
            var products = await _productService.GetProductsAsync(true);
            var productsViewModels = _mapper.Map<IEnumerable<Product>, ObservableCollection<ProductViewModel>>(products.OrderBy(p => p.Price));

            ProductList = productsViewModels;
            IsBusy = false;
        }
    }
}
