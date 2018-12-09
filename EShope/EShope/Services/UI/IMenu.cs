using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.UI.Imp
{
    public interface IMenu
    {
        Action LogoutAction { get; }
        Action GoToHomeAction { get; }
        Action SyncProducts { get; }
    }
}
