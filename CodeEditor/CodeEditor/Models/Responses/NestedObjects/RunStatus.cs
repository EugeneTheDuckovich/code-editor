using Newtonsoft.Json;

namespace CodeEditor.Models.Responses;

public enum StatusCode
{
    NA,
    AC,
    MLE,
    TLE,
    RE,
}

public enum StatusDetailsCode
{
    NA,
    SIGXFSZ,
    SIGSEGV,
    SIGFPE,
    SIGBUS,
    SIGABRT,
    NZEC,
    OTHER,
}

public class RunStatus
{
    [JsonProperty("status")]
    public StatusCode? Status { get; set; }
    
    [JsonProperty("status_detail")]
    public StatusDetailsCode? StatusDetail { get; set; }
    
    [JsonProperty("memory_used")]
    public int? MemoryUsed { get; set; }

    [JsonProperty("time_used")]
    public double? TimeUsed { get; set; }
    
    [JsonProperty("output")]
    public string? OutputPath { get; set; }
    
    [JsonProperty("request_NOT_OK_reason")]
    public string? RequestFailureReason { get; set; }
    
    [JsonProperty("exit_code")]
    public int? ExitCode { get; set; }
}