using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestEaseSample;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly IGithubApi githubApi;
        public GithubController(IGithubApi githubApi)
        {
            this.githubApi = githubApi;
        }

        public async Task<User> Get()
        {
            return await githubApi.GetUserAsyn();
        }
    }
}
