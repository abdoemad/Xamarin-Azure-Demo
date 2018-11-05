using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Common
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
