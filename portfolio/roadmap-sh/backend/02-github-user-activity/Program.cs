using System.Text.Json;

public class Program
{
    public static HttpClient client = new();

    public static async Task Main(string[] args)
    {   
        if(args.Length <= 0)
        {
            Console.WriteLine("Please provide a GitHub user");
            return;
        }

        string user = args[0];
        Console.WriteLine($"Fetching {user} GitHub data...");
        client.BaseAddress = new Uri("https://api.github.com");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version","2026-03-10");
        client.DefaultRequestHeaders.Add("User-Agent","agent");

        try
        {   
            HttpResponseMessage response = await client.GetAsync($"users/{user}/events/public");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Github API says {(int)response.StatusCode} {response.ReasonPhrase}");
                return;
            }

            string json = await response.Content.ReadAsStringAsync();


            JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            if(root.ValueKind != JsonValueKind.Array)
            {
                Console.WriteLine("Unexpected JSON value. Expected value: Array");
                return;
            }

            if(root.GetArrayLength() <= 0)
            {
                Console.WriteLine("No recent activity was found");
                return;
            }

            foreach(JsonElement entry in root.EnumerateArray())
            {
                string type = entry.GetProperty("type").GetString() ?? string.Empty;
                
                string repoName = string.Empty;
                if(entry.TryGetProperty("repo", out JsonElement repo) && repo.TryGetProperty("name", out JsonElement nameProp))
                {
                    repoName = nameProp.GetString() ?? string.Empty;
                }

                JsonElement? payload = null;
                if(entry.TryGetProperty("payload", out JsonElement payloadElement)){
                    payload = payloadElement;
                }

                switch (type)
                {
                    case "PushEvent":
                        DisplayPushEvent(payload,repoName);
                        break;
                    case "IssuesEvent":
                        DisplayIssuesEvent(payload,repoName);
                        break;
                    case "WatchEvent":
                        DisplayWatchEvent(repoName);
                        break;
                    case "ForkEvent":
                        DisplayForkEvent(payload,repoName);
                        break;
                    case "CreateEvent":
                        DisplayCreateEvent(payload,repoName);
                        break;
                    default:
                    Console.WriteLine($"{type} in {repoName}");
                    break;
                }
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }


    public static string SearchStringProp(JsonElement? element, string key)
    {
        if(element?.TryGetProperty(key, out JsonElement prop) == true)
        {
            return prop.GetString() ?? string.Empty;
        }
        else
        {
            return string.Empty;
        }
    }
    #region Display Events
    static void DisplayPushEvent(JsonElement? payload, string repoName)
    {
        string refValue = SearchStringProp(payload,"ref");

        string branch = refValue.StartsWith("refs/heads/")?
            refValue.Substring("refs/heads/".Length) : 
            string.Empty;

        if(!(branch == string.Empty) && !branch.IsWhiteSpace())
        {
            Console.WriteLine($"Pushed to {branch} in {repoName}");
        }
        else
        {
            Console.WriteLine($"Pushed to {repoName}");
        }
    }
  
    private static void DisplayIssuesEvent(JsonElement? payload, string repoName)
    {
        string actionValue = SearchStringProp(payload,"action");
        string titleValue = SearchStringProp(payload?.GetProperty("issue"),"title");

        Console.WriteLine($"In {repoName} {actionValue} issue {titleValue}");
    }
    private static void DisplayCreateEvent(JsonElement? payload, string repoName)
    {
        string resourceRef = SearchStringProp(payload,"ref");
        string typeRef = SearchStringProp(payload, "ref_type");

        if(resourceRef != string.Empty)
        {
            Console.WriteLine($"Created {typeRef} {resourceRef} at {repoName}");
        }
        else
        {
            Console.WriteLine($"Created {typeRef} - {repoName}");
        }

    }

    private static void DisplayForkEvent(JsonElement? payload, string repoName)
    {
        string forkeeValue = SearchStringProp(payload?.GetProperty("forkee"),"full_name");

        if(forkeeValue == string.Empty)
        {
            Console.WriteLine($"Forked in {repoName}");
        }
        else{
            Console.WriteLine($"Forked {repoName} into {forkeeValue}");
        }
    }

    private static void DisplayWatchEvent(string repoName)
    {
        Console.WriteLine($"Starred {repoName}");
    }
    #endregion
}