using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EShope.Pages.Base
{
    [ContentProperty(nameof(ContentPlaceHolder))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPage : PageBase
	{
		public LoadingPage ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty MainContentProperty =
        BindableProperty.Create(nameof(ContentPlaceHolder), typeof(View), typeof(LoadingPage));

        public View ContentPlaceHolder
        {
            get { return (View)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
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