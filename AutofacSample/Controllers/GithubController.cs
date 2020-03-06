using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestEaseSample;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly IGithubApi _githubApi;
        public GithubController(IGithubApi githubApi)
        {
            _githubApi = githubApi;
        }

        public async Task<ActionResult<User>> Get()
        { 
            var user = await _githubApi.GetUserAsyn();
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
