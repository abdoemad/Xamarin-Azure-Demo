using EShope.Models;
using EShope.Services.Data;
using EShope.Services.Data.Models;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;
using EShope.Services.UI;
using EShope.Services.Device;

namespace EShope.ViewModels
{
    public class ProductCatalogViewModel : ViewModelBase
    {
        IConnectionService _connectionService;
        private readonly IProductService _productService;
        private readonly INavigationService _navigationService;
        private readonly Services.Infra.IMapper _mapper;
        private ObservableCollection<ProductViewModel> _productCatalogList;

        public ObservableCollection<ProductViewModel> ProductCatalogList
        {
            get => _productCatalogList;
            set
            {
                _productCatalogList = value;
                RaisePropertyChanged(() => ProductCatalogList);
            }
        }
        public ProductCatalogViewModel(IProductService productService, Services.Infra.IMapper mapper, INavigationService navigationService, IConnectionService connectionService)
        {
            _connectionService = connectionService;
            _navigationService = navigationService;
            _productService = productService;
            _mapper = mapper;

            var mapConfig = new Dictionary<Type, Type>
            {
                { typeof(Product), typeof(ProductViewModel) }
            };
            mapper.Initialize(mapConfig);
        }
        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            
            var products = await _productService.GetProductsAsync(false);

            if (products.Count == 0)
            {
                var isInternetConnected = _connectionService.IsConnected;
                if (isInternetConnected)
                    products = await _productService.GetProductsAsync(true);
            }

            var productsViewModels = _mapper.Map<IEnumerable<Product>, ObservableCollection<ProductViewModel>>(products.OrderBy(p => p.Price));

            ProductCatalogList = productsViewModels;
            IsBusy = false;
        }
        private readonly ICommand _productSelectionCommand;
        public ICommand ProductSelectionCommand => _productSelectionCommand ?? new Command<ProductViewModel>(product =>
         {
             _navigationService.NavigateTo<ProductDetailsViewModel>(product, true);
         });
    }
}
