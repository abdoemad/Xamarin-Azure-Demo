using Abp.Application.Services;

namespace Eshope.Admin
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AdminAppServiceBase : ApplicationService
    {
        protected AdminAppServiceBase()
        {
            LocalizationSourceName = AdminConsts.LocalizationSourceName;
        }
    }
}