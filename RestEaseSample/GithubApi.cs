using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestEase;

namespace RestEaseSample
{
    public interface IGithubApi
    {
        Task<User> GetUserAsyn();
    }
    public class GithubApi: IGithubApi
    {
        public GithubApi()
        {
        }

        public Task<User> GetUserAsyn()
        {
            // Create an implementation of that interface
            // We'll pass in the base URL for the API
            IGitHubContract api = RestClient.For<IGitHubContract>("https://api.github.com");

            // Now we can simply call methods on it
            // Normally you'd await the request, but this is a console app
            return api.GetUserAsync("canton7");
        }
    }
}
