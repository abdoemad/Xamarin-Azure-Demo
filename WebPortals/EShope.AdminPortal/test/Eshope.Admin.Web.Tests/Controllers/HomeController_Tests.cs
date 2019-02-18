using System.Threading.Tasks;
using Eshope.Admin.Web.Controllers;
using Shouldly;
using Xunit;

namespace Eshope.Admin.Web.Tests.Controllers
{
    public class HomeController_Tests: AdminWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
