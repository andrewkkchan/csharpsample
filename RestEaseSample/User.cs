using System;
using Newtonsoft.Json;

namespace RestEaseSample
{
    // We receive a JSON response, so define a class to deserialize the json into
    public class User
    {
        public string Name { get; set; }
        public string Blog { get; set; }

        // This is deserialized using Json.NET, so use attributes as necessary
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
