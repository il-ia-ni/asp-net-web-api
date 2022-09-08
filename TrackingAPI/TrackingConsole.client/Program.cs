using System.Collections;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TrackingConsole.client;

HttpClient client = new();  // HttpClient cls is used in .Net to make HTTP requests and comes with built-in methods for this

client.BaseAddress = new Uri("https://localhost:7070");  // sets as a base URL for HTTP reqs the url of the Web API project(located in Properties/launchSettings.json -> Profiles/Tracking_api)

// tells the server the result of reqs must be in JSON format
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

HttpResponseMessage response = await client.GetAsync("api/issue");  // a url of the endpoint is required to make the async get req
response.EnsureSuccessStatusCode();  // sets IsSuccessStatusCode to true if the response was successful

if (response.IsSuccessStatusCode)
{
    var issues = await response.Content.ReadFromJsonAsync <IEnumerable <IssueDto>> ();  // Content prop's method ReadFromJsonAsync deserializes the content of the response into IEnumerable of IssueDto (Data Transfer Object == Model of API)

    foreach (var issue in issues)
    {
        Console.WriteLine(issue.Title);
    };
}
else
{
    Console.WriteLine("No result found. Check your connection to the Database");
}

Console.ReadLine();

