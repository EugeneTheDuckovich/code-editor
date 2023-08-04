using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeEditor.Contracts;
using CodeEditor.Models.Requests;
using CodeEditor.Models.Responses;
using CodeEditor.Services.Abstract;
using CodeEditor.ViewModels.Abstract;
using CommunityToolkit.Mvvm.Input;

namespace CodeEditor.ViewModels;

public class CodePageViewModel : PageViewModel
{
    private readonly ICodeExecutionService _codeExecutionService;
    private string _code;
    private int _memoryLimitKb;
    private int _timeLimitSeconds;
    private string _selectedLanguage;
    private string _compilationStatus;
    private string _output;

    public string Code
    {
        get => _code;
        set
        {
            _code = value;
            OnPropertyChanged();
        }
    }

    public string CompilationStatus
    {
        get => _compilationStatus;
        set
        {
            _compilationStatus = value;
            OnPropertyChanged();
        }
    }

    public string Output
    {
        get => _output;
        set
        {
            _output = value;
            OnPropertyChanged();
        }
    }

    public int MemoryLimitKb
    {
        get => _memoryLimitKb;
        set
        {
            _memoryLimitKb = value;
            OnPropertyChanged();
        }
    }

    public int TimeLimitSeconds
    {
        get => _timeLimitSeconds;
        set
        {
            _timeLimitSeconds = value;
            OnPropertyChanged();
        }
    }

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            _selectedLanguage = value;
            OnPropertyChanged();
        }
    }
    
    public IEnumerable<string> Languages => LanguageDictionary.Languages.Keys;

    public ICommand ExecuteCodeCommand { get; }

    public CodePageViewModel(ICodeExecutionService codeExecutionService)
    {
        _codeExecutionService = codeExecutionService;
        
        Code = Output = CompilationStatus = String.Empty;
        MemoryLimitKb = TimeLimitSeconds = 0;
        
        ExecuteCodeCommand = new RelayCommand(ExecuteCode);
    }

    private async void ExecuteCode()
    {
        var request = new ExecuteCodeRequest
        {
            SourceCode = Code,
            Language = LanguageDictionary.Languages[SelectedLanguage],
            MemoryLimitKb = 262144,
            TimeLimitSeconds = 5
        };
        ExecutionStatusResponse response = await _codeExecutionService.StartExecution(request);
        InitializeStatus(response);
        UpdateStatus(response);
    }

    private async void UpdateStatus(ExecutionStatusResponse response)
    {
        bool isCompleted = false;
        while (!isCompleted)
        {
            await Task.Delay(100);
            response = await _codeExecutionService.UpdateExecutionStatus(response);
            
            isCompleted = response.ReguestStatus.Code is RequestStatusCode.REQUEST_COMPLETED 
                or RequestStatusCode.REQUEST_FAILED;
            InitializeStatus(response);
        }
    }

    private async void InitializeStatus(ExecutionStatusResponse response)
    {
        var httpClient = new HttpClient();
        string? outputPath = response.Result?.RunStatus?.OutputPath;
        if(!string.IsNullOrEmpty(outputPath))
            Output = await httpClient.GetStringAsync(outputPath);
        CompilationStatus = response.Result?.CompileStatus ?? string.Empty;
    }
}