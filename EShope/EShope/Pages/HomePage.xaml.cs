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
	public partial class HomePage : PageBase
	{
		public HomePage ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (DesignMode.IsDesignModeEnabled)
            {
                this.BindingContext = new List<Product> { new Product { Name = "Test", Description = "Desc", Price = 50 } };
            }
        }
    }
}