using Newtonsoft.Json;

namespace CodeEditor.Models.Requests;

public class ExecuteCodeRequest
{
    [JsonProperty("lang")]
    public string Language { get; set; }
    
    [JsonProperty("source")]
    public string SourceCode { get; set; }
    
    [JsonProperty("input")]
    public string Input { get; set; }
    
    [JsonProperty("memory_limit")]
    public int MemoryLimitKb { get; set; }
    
    [JsonProperty("time_limit")]
    public int TimeLimitSeconds { get; set; }
    
    [JsonProperty("context")]
    public string Context { get; set; }
    
    [JsonProperty("callback")]
    public string CallbackUrl { get; set; }
}