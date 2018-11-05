using EShope.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EShope.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShoppingBarView : ContentView
	{
		public ShoppingBarView ()
		{
			InitializeComponent ();
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var viewModel = this.BindingContext as ShoppingBarViewModel;

            await viewModel.NavigateToShoppingCartCommand.ExecuteAsync();
            
        }
    }
}