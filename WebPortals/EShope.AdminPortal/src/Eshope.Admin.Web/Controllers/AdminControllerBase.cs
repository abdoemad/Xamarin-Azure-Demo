using Abp.AspNetCore.Mvc.Controllers;

namespace Eshope.Admin.Web.Controllers
{
    public abstract class AdminControllerBase: AbpController
    {
        protected AdminControllerBase()
        {
            LocalizationSourceName = AdminConsts.LocalizationSourceName;
        }
    }
}