using EShope.Models;
using EShope.Services.Data;
using EShope.Services.Data.Models;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EShope.ViewModels
{
    public class ProductCatalogViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly Services.Infra.IMapper _mapper;
        private ObservableCollection<Models.Product> _productList;
        public ObservableCollection<Models.Product> ProductList
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

            var mapConfig = new Dictionary<Type, Type> { { typeof(Services.Data.Models.Product), typeof(Models.Product) } };
            mapper.Initialize(mapConfig);

            
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            var products = _productService.GetProducts();
            var productsViewModel = _mapper.Map<List<Services.Data.Models.Product>, List<Models.Product>>(products);
            ProductList = new ObservableCollection<Models.Product>(productsViewModel);
            IsBusy = false;
        }
    }
}
