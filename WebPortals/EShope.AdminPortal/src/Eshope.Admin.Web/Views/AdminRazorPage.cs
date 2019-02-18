using Abp.AspNetCore.Mvc.Views;

namespace Eshope.Admin.Web.Views
{
    public abstract class AdminRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected AdminRazorPage()
        {
            LocalizationSourceName = AdminConsts.LocalizationSourceName;
        }
    }
}
