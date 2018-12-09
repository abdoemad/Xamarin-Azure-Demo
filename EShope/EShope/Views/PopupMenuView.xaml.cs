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
	public partial class PopupMenuView : ContentView
	{
		public PopupMenuView()
		{
			InitializeComponent ();
		}

        private void PopupMenu_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}