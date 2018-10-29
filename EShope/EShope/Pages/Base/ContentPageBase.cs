using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EShope.Pages.Base
{
    public class ContentPageBase : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as BaseViewModel;
            viewModel.OnAppearing();
        }
    }
}
