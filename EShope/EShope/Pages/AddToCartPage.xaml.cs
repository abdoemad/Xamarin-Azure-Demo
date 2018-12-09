using EShope.Pages.Base;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EShope.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddToCartPage : ShopppingBasePage // ShopppingBarContainerPage //PageBase
    {
		public AddToCartPage()
		{
            
			InitializeComponent ();
		}

        //public ProductDetailsPage(ViewModelBase viewModel) : base(viewModel)
        //{
        //    InitializeComponent();
        //    this.BindingContext = viewModel;
        //}
    }
}