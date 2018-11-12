using EShope.Repository;
using EShope.Services.Data;
using EShope.Services.Data.Imp;
using EShope.Services.Device;
using EShope.Services.Device.Imp;
using EShope.Services.Infra;
using EShope.Services.Infra.Imp;
using EShope.Services.UI;
using EShope.Services.UI.Imp;
using EShope.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xamarin.Forms;
//using Xamarin.Forms;
//using CommonServiceLocator;
//using GalaSoft.MvvmLight;

namespace EShope
{
    //  Bootstrapper
    public class ViewModelLocator
    {
        static SimpleIoc _container;

        public ViewModelLocator()
        {
            _container = SimpleIoc.Default;
            //App.LoggedInUser = new Models.UserViewMode { UserName = "Test user", IsOnlineAuthenticate = false };
            #region Design Mode
            if (DesignMode.IsDesignModeEnabled)
            {
            }
            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            #endregion
            RegisterDependancies();
            //MessagingCenter.Instance.
        }
        private void RegisterDependancies()
        {
            //--- Device
            _container.Register<IConnectionService, ConnectionService>();

            //--- Infra
            _container.Register<IMapper, Services.Infra.Imp.AutoMapper>();
            _container.Register<IAPIConsumer, APIConsumer>();
            _container.Register<IAuthenticationService, AuthenticationService>();

            //--- UI
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IDialogService, DialogService>();

            //--- Data Services
            _container.Register<IProductService, ProductService>();
            _container.Register<IOrderService, OrderService>();
            //_container.Register<IRepository<Product>, Repository<Product>>();

            //-- ViewModels
            _container.Register<LoginViewModel>();
            _container.Register<ProductCatalogViewModel>();
            //_container.Register<ProductDetailsViewModel>();

            _container.Register<ShoppingCartViewModel>(true);
            _container.Register<ShoppingBarViewModel>(true);
        }

        public static T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        public bool IsPreview => ViewModelBase.IsInDesignModeStatic;

        #region ViewModels Resolving
        public LoginViewModel Login => SimpleIoc.Default.GetInstance<LoginViewModel>();
        public ProductCatalogViewModel ProductCatalog => SimpleIoc.Default.GetInstance<ProductCatalogViewModel>();
        public ProductDetailsViewModel ProductDetails => SimpleIoc.Default.GetInstance<ProductDetailsViewModel>();
        public ShoppingCartViewModel ShoppingCart => SimpleIoc.Default.GetInstance<ShoppingCartViewModel>();
        public ShoppingBarViewModel ShoppingBar => SimpleIoc.Default.GetInstance<ShoppingBarViewModel>();
        #endregion
    }
}
