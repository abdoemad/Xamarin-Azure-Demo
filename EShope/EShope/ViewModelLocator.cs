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
        private SimpleIoc _container;

        public ViewModelLocator()
        {
            _container = SimpleIoc.Default;
            if (ViewModelBase.IsInDesignModeStatic)
            {

            }

            // ViewModels
            _container.Register<LoginViewModel>();
            _container.Register<INavigationService, NavigationService>();

            

        }

        public T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }

        #region ViewModels
        public LoginViewModel Login => SimpleIoc.Default.GetInstance<LoginViewModel>();
        #endregion
    }
}
