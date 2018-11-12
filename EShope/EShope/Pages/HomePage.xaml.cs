using EShope.Models;
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
	public partial class HomePage : ShopppingBarContainerPage //LoadingPage // PageBase
    {
		public HomePage ()
		{
            InitializeComponent();
            this.productCatalogList.ItemTapped += ProductCatalogList_ItemTapped;
        }

        private void ProductCatalogList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.productCatalogList.SelectedItem = null;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (DesignMode.IsDesignModeEnabled)
        //    {
        //        this.BindingContext = new List<ProductViewModel> { new ProductViewModel { Name = "Test", Description = "Desc", Price = 50 } };
        //    }
        //}
    }
}