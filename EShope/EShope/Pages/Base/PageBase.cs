using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EShope.Pages.Base
{
    public class PageBase : ContentPage
    {
        public PageBase()
        {

            //Content = new Grid {

            //};
            //this.Padding = Device.RuntimePlatform == Device.iOS ? new Thickness(0, 20, 0, 0) : new Thickness();

            //this.Content.Margin = Device.RuntimePlatform == Device.iOS ? new Thickness(0, 20, 0, 0) : new Thickness();
        }
        public PageBase(ViewModelBase viewModel)
        {
            //this.BindingContext = viewModel;
        }
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
