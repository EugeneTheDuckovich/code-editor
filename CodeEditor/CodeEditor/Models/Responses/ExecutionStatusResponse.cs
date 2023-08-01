using Newtonsoft.Json;

namespace CodeEditor.Models.Responses;

public class ExecutionStatusResponse
{
    [JsonProperty("request_status")]
    public ReguestStatus ReguestStatus { get; set; }

    [JsonProperty("he_id")]
    public string ClientId { get; set; }
    
    [JsonProperty("status_update_url")]
    public string StatusUpdateUrl { get; set; }
    
    [JsonProperty("context")]
    public string? Context { get; set; }
    
    [JsonProperty("result")]
    public ExecutionResult Result { get; set; }
}


    
