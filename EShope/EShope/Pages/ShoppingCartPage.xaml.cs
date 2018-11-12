using EShope.Pages.Base;
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
	public partial class ShoppingCartPage : ShopppingBarContainerPage //LoadingPage // PageBase
    {
		public ShoppingCartPage ()
		{
			InitializeComponent ();
            this.cartList.ItemSelected += CartList_ItemSelected;
		}

        private void CartList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            cartList.SelectedItem = null;
        }
    }
}