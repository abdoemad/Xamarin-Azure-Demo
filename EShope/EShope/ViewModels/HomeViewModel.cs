using EShope.Models;
using EShope.Services.Data;
using EShope.Services.Data.Models;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EShope.ViewModels
{
    public class HomeViewModel : BaseViewModel
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
        public HomeViewModel(IProductService productService, Services.Infra.IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

            var mapConfig = new Dictionary<Type, Type> { { typeof(Product), typeof(ProductViewModel) } };
            mapper.Initialize(mapConfig);

            
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            var products = _productService.GetProducts();
            var productsViewModel = _mapper.Map<List<Product>, List<ProductViewModel>>(products);
            ProductList = new ObservableCollection<ProductViewModel>(productsViewModel);
            IsBusy = false;
        }
    }
}
