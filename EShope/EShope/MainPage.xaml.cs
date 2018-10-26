using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EShope
{
    public partial class MainPage : NavigationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(Page homePage) : base(homePage)
        {
            
        }
    }
}
