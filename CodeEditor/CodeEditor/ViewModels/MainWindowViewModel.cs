using System;
using CodeEditor.Contracts.Abstract;
using CodeEditor.Mvvm;
using CodeEditor.Services.Abstract;
using CodeEditor.ViewModels.Abstract;

namespace CodeEditor.ViewModels;

public class MainWindowViewModel : NotifyPropertyChanged
{
    private readonly ICodeExecutionService _codeExecutionService;
    private readonly ILanguageDictionary _languageDictionary;
    private PageViewModel _currentViewModel;

    public PageViewModel CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel(ICodeExecutionService codeExecutionService, ILanguageDictionary languageDictionary)
    {
        _codeExecutionService = codeExecutionService;
        _languageDictionary = languageDictionary;
        ChangeView(ViewType.CodePage);
    }

    private void ChangeView(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.CodePage:
                CurrentViewModel = new CodePageViewModel(_codeExecutionService,_languageDictionary);
                break;
            default:
                throw new ArgumentException("invalid view type!");
        }

        CurrentViewModel.ViewChanged += ChangeView;
    }
}