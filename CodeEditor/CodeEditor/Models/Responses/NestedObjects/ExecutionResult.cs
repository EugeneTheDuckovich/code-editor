using Newtonsoft.Json;

namespace CodeEditor.Models.Responses;


public class ExecutionResult
{
    [JsonProperty("run_status")]
    public RunStatus? RunStatus { get; set; }
    
    [JsonProperty("compile_status")]
    public string? CompileStatus { get; set; }
}