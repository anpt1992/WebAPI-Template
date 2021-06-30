using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI_Template.Contracts;
using WebAPI_Template.Contracts.V1;
using WebAPI_Template.Contracts.V1.Requests;
using WebAPI_Template.Domain;
using Xunit;

namespace WebAPI_Template.IntergrationTests
{
    public class TestControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyTests_ReturnEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();

        }

        [Fact]
        public async Task Get_ReturnsTest_WhenTestExistsInDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdTest = await CreateTestAsync(new CreatePostRequest { Name = "Test" });

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{testId}", createdTest.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedTest = await response.Content.ReadAsAsync<Post>();
            returnedTest.Id.Should().Be(createdTest.Id);
            returnedTest.Name.Should().Be("Test");
        }
    }
}
