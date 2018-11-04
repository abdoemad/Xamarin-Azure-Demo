using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.UI
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        Task ShowDialog(string message, string title, string acceptbtnLabel, string cancelbtnLabel);

        
    }
}
