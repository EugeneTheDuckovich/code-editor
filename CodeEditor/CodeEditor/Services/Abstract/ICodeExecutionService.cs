using System.Threading.Tasks;
using CodeEditor.Models.Requests;
using CodeEditor.Models.Responses;

namespace CodeEditor.Services.Abstract;

public interface ICodeExecutionService
{
    Task<ExecutionStatusResponse> StartExecution(ExecuteCodeRequest request);

    Task<ExecutionStatusResponse> UpdateExecutionStatus(ExecutionStatusResponse response);
}