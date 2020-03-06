using System;
using System.Threading.Tasks;
using AutofacSample.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestEaseSample;
using Xunit;

namespace TestSample
{
    public class GithubUnitTest
    {
        public GithubUnitTest()
        {
        }
        [Fact]
        public async Task TestGet()
        {
            var mockApi = new Mock<IGithubApi>();
            mockApi.Setup(m => m.GetUserAsyn()).Returns(Task.FromResult(new User {
                Name = "Antony Male"
            }));
            GithubController githubController = new GithubController(mockApi.Object);
            ActionResult<User> user = await githubController.Get();
            Assert.Equal("Antony Male", user.Value.Name);

        }
    }
}
