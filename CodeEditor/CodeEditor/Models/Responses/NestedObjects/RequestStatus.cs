using Newtonsoft.Json;

namespace CodeEditor.Models.Responses;

public enum RequestStatusCode
{
    REQUEST_INITIATED,
    REQUEST_QUEUED,
    CODE_COMPILED,
    REQUEST_COMPLETED,
    REQUEST_FAILED,
}

public class ReguestStatus
{
    [JsonProperty("message")]
    public string Message { get; set; }
    
    [JsonProperty("code")]
    public RequestStatusCode Code { get; set; }
}