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
	public partial class ShopppingBasePage : PageBase
	{
		public ShopppingBasePage ()
		{
			InitializeComponent ();
		}
        public static readonly BindableProperty IsMenuVisibleProperty = BindableProperty.Create(nameof(IsMenuVisible), typeof(bool), typeof(ShopppingBasePage));

        public bool IsMenuVisible
        {
            get { return (bool)GetValue(IsMenuVisibleProperty); }
            set
            {
                this.popupMenu.IsVisible = value;
                SetValue(IsMenuVisibleProperty, value);

                if (value)
                    relativeLayout.RaiseChild(popupMenu);
            }
        }

        public static readonly BindableProperty ContentPlaceHolderProperty = BindableProperty.Create(nameof(ContentPlaceHolder), typeof(View), typeof(ShopppingBasePage));

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

        private void PopupMenu_Unfocused(object sender, FocusEventArgs e)
        {
            IsMenuVisible = false;
        }

        public Task ToggleMenuVisibility()
        {
            IsMenuVisible = !IsMenuVisible;
            return Task.CompletedTask;
        }

        public void ShowMenu()
        {
            IsMenuVisible = true;
        }
        public void HideMenu()
        {
            IsMenuVisible = false;
        }
    }
}