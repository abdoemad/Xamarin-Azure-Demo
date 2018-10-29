using EShope.Services.Data;
using EShope.Services.Data.Imp;
using EShope.Services.Infra;
using EShope.Services.Infra.Imp;
using EShope.Services.UI;
using EShope.Services.UI.Imp;
using EShope.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
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
            //if (ViewModelBase.IsInDesignModeStatic)
            //{

            //}

            //--- Infra
            _container.Register<IMapper, Services.Infra.Imp.AutoMapper>();
            _container.Register<IAPIConsumer, APIConsumer>();
            _container.Register<IAuthenticationService, AuthenticationService>();

            //--- UI
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IDialogService, DialogService>();

            //--- Data Services
            _container.Register<IProductService, ProductService>();

            //-- ViewModels
            _container.Register<LoginViewModel>();
            _container.Register<HomeViewModel>();

            
        }
        public static T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        #region ViewModels
        public LoginViewModel Login => SimpleIoc.Default.GetInstance<LoginViewModel>();

        public HomeViewModel Home => SimpleIoc.Default.GetInstance<HomeViewModel>();
        #endregion
    }
}
