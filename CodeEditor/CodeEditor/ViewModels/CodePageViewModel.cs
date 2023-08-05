using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeEditor.Contracts;
using CodeEditor.Contracts.Abstract;
using CodeEditor.Models.Requests;
using CodeEditor.Models.Responses;
using CodeEditor.Services.Abstract;
using CodeEditor.ViewModels.Abstract;
using CommunityToolkit.Mvvm.Input;

namespace CodeEditor.ViewModels;

public class CodePageViewModel : PageViewModel
{
    private readonly ICodeExecutionService _codeExecutionService;
    private readonly ILanguageDictionary _languageDictionary;
    private string _code;
    private int _memoryLimitKb;
    private int _timeLimitSeconds;
    private string _selectedLanguage;
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
    
    public IEnumerable<string> Languages => _languageDictionary.Languages.Keys;

    public ICommand ExecuteCodeCommand { get; }

    public CodePageViewModel(ICodeExecutionService codeExecutionService, ILanguageDictionary languageDictionary)
    {
        _codeExecutionService = codeExecutionService;
        _languageDictionary = languageDictionary;
        
        Code = Output =  String.Empty;
        MemoryLimitKb = 262144;
        TimeLimitSeconds = 5;
        
        ExecuteCodeCommand = new RelayCommand(ExecuteCode);
    }

    private async void ExecuteCode()
    {
        if (string.IsNullOrEmpty(Code)) return;
        
        var request = new ExecuteCodeRequest
        {
            SourceCode = Code,
            Language = _languageDictionary.Languages[SelectedLanguage],
            MemoryLimitKb = _memoryLimitKb < 1 ? 1 : _memoryLimitKb,
            TimeLimitSeconds =  _timeLimitSeconds < 1 ? 1 : _timeLimitSeconds,
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
                or RequestStatusCode.REQUEST_FAILED
                || response.Result.CompileStatus.ToLower().Contains("error");
            InitializeStatus(response);                                       
        }
    }

    private async void InitializeStatus(ExecutionStatusResponse response)
    {
        var httpClient = new HttpClient();
        string? outputPath = response.Result?.RunStatus?.OutputPath;
        string compilationStatus = $"Compilation status: {response.Result?.CompileStatus ?? string.Empty}\r\n";
        string output = string.IsNullOrEmpty(outputPath) ? string.Empty: await httpClient.GetStringAsync(outputPath);
        Output = compilationStatus + output;
    }
}