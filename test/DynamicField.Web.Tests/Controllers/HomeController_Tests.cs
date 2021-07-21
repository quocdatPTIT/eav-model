using System.Threading.Tasks;
using DynamicField.Models.TokenAuth;
using DynamicField.Web.Controllers;
using Shouldly;
using Xunit;

namespace DynamicField.Web.Tests.Controllers
{
    public class HomeController_Tests: DynamicFieldWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}