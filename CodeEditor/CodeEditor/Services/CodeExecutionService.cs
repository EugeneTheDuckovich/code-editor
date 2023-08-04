using System;
using System.Net.Http;
using System.Threading.Tasks;
using CodeEditor.Models.Requests;
using CodeEditor.Models.Responses;
using CodeEditor.Services.Abstract;
using Newtonsoft.Json;
using System.Configuration;

namespace CodeEditor.Services;

public class CodeExecutionService : ICodeExecutionService
{
    private string ApiUrl
    {
        get => ConfigurationManager.AppSettings.Get("ApiUrl") ?? string.Empty;
    }

    private string ApiKey
    {
        get => ConfigurationManager.AppSettings.Get("ApiKey") ?? string.Empty;
    }
    
    public async Task<ExecutionStatusResponse> StartExecution(ExecuteCodeRequest request)
    {
        string jsonRequest = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonRequest);
        content.Headers.Clear();
        content.Headers.Add("content-type", "application/json");
        content.Headers.Add("client-secret", ApiKey);
        
        var httpClient = new HttpClient();
        HttpResponseMessage responseMessage = await httpClient.PostAsync(ApiUrl,content);
        string responseJson = await responseMessage.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ExecutionStatusResponse>(responseJson)??
               throw new Exception("invalid response!");
    }

    public async Task<ExecutionStatusResponse> UpdateExecutionStatus(ExecutionStatusResponse response)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("client-secret",ApiKey);
        HttpResponseMessage updatedStatusMessage = await httpClient.GetAsync(response.StatusUpdateUrl);
        string responseJson = await updatedStatusMessage.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ExecutionStatusResponse>(responseJson)??
               throw new Exception("invalid response!");
    }
}