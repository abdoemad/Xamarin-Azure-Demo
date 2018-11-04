using EShope.Pages.Base;
using EShope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.UI
{
    public interface INavigationService
    {
        Task Initialize();

        Task NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

        Task NagigatoToHomePage();
    }
}
