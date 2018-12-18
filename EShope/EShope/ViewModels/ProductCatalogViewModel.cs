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
    public class ProductCatalogViewModel : ShoppingViewModelBase
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

            MessagingCenter.Subscribe<object>(this, "Sync request", async param =>
            {
                await SyncProducts();
            });
        }
        public override async Task InitializeAsync(object data)
        {
            var syncFlag = false;
            if (data != null)
                syncFlag = (bool)data;

            IsBusy = true;

            IList<Product> products = null;
            if(!syncFlag)
                products = await _productService.GetProductsAsync(false);

            if (products == null || products.Count == 0 || syncFlag)
            {
                var isInternetConnected = _connectionService.IsConnected;
                if (isInternetConnected)
                    products = await _productService.GetProductsAsync(true);
            }

            var productsViewModels = _mapper.Map<IEnumerable<Product>, ObservableCollection<ProductViewModel>>(products.OrderBy(p => p.Price));

            ProductCatalogList = productsViewModels;
            IsBusy = false;
        }
        public async Task SyncProducts()
        {
            await InitializeAsync(true);
        }

        private readonly ICommand _productSelectionCommand;
        public ICommand ProductSelectionCommand => _productSelectionCommand ?? new Command<ProductViewModel>(product =>
         {
             _navigationService.NavigateTo<AddToCartViewModel>(product, true);
         });
    }
}
