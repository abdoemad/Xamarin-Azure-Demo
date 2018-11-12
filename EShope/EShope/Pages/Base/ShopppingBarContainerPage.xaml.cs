using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EShope.Pages.Base
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShopppingBarContainerPage : PageBase
	{
		public ShopppingBarContainerPage ()
		{
			InitializeComponent ();
		}
        public static readonly BindableProperty ContentPlaceHolderProperty =
        BindableProperty.Create(nameof(ContentPlaceHolder), typeof(View), typeof(LoadingPage));

        public View ContentPlaceHolder
        {
            get { return (View)GetValue(ContentPlaceHolderProperty); }
            set { SetValue(ContentPlaceHolderProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ContentPlaceHolder == null)
            {
                return;
            }
            SetInheritedBindingContext(ContentPlaceHolder, BindingContext);
        }
    }
}