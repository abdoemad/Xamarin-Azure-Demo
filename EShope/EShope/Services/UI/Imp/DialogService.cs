using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EShope.Services.UI.Imp
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string cancelbtnLabel)
        {
            return App.AppMainPage.DisplayAlert(message, title, cancelbtnLabel);
        }

        public Task ShowDialog(string message, string title, string acceptbtnLabel, string cancelbtnLabel)
        {
            return App.AppMainPage.DisplayAlert(message, title, acceptbtnLabel, cancelbtnLabel);
        }

    }
}
