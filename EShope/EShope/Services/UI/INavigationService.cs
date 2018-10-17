using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.UI
{
    public interface INavigationService
    {
        Task Initialize();

        Task NavigateTo(Type viewModelType);

        Task NagigatoToHomePage();
    }
}
