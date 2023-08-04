using System;
using Newtonsoft.Json;

namespace CodeEditor.Models.Requests;

public class ExecuteCodeRequest
{
    [JsonProperty("source")]
    public string SourceCode { get; set; }
    
    [JsonProperty("lang")]
    public string Language { get; set; }
    
    [JsonProperty("memory_limit")]
    public int MemoryLimitKb { get; set; }
    
    [JsonProperty("time_limit")]
    public int TimeLimitSeconds { get; set; }
}