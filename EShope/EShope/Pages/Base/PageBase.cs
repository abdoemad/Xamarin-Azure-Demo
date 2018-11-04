using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EShope.Pages.Base
{
    public class PageBase : ContentPage
    {
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var viewModel = this.BindingContext as ViewModelBase;
        //    viewModel.OnAppearing();
        //}

        public static implicit operator PageBase(Type v)
        {
            throw new NotImplementedException();
        }
    }
}
