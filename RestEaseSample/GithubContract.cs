using System;
using System.Threading.Tasks;
using RestEase;

namespace RestEaseSample
{
    // Define an interface representing the API
    // GitHub requires a User-Agent header, so specify one
    [Header("User-Agent", "RestEase")]
    public interface IGitHubContract
    {
        // The [Get] attribute marks this method as a GET request
        // The "users" is a relative path the a base URL, which we'll provide later
        // "{userId}" is a placeholder in the URL: the value from the "userId" method parameter is used
        [Get("users/{userId}")]
        Task<User> GetUserAsync([Path] string userId);
    }
}
