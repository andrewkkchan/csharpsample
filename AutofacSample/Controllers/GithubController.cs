using System.Threading.Tasks;
using AutofacSample.Queue;
using Microsoft.AspNetCore.Mvc;
using RestEaseSample;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly IGithubApi _githubApi;
        private readonly IQueueProvider<User> _queueProvider;
        public GithubController(IGithubApi githubApi, IQueueProvider<User> queueProvider)
        {
            _githubApi = githubApi;
            _queueProvider = queueProvider;
        }

        public async Task<ActionResult<User>> Get()
        { 
            var user = await _githubApi.GetUserAsyn();
            if (user != null)
            {
                _queueProvider.SendMessage(user);
                return Ok(user);
            }
            return NotFound();
        }
    }
}
