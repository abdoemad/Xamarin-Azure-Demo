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
        Task ClearStack();
        Task Initialize();
        Task NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateTo<TViewModel>(object paramter, bool initiateViewModel) where TViewModel : ViewModelBase;
        Task NagigatoToHomePage();
        Task NavigateToLoginPage();
        Task NavigateBackAsync();
    }
}
