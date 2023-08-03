using System;
using CodeEditor.Mvvm;
using CodeEditor.Services.Abstract;
using CodeEditor.ViewModels.Abstract;

namespace CodeEditor.ViewModels;

public class MainWindowViewModel : NotifyPropertyChanged
{
    private readonly ICodeExecutionService _codeExecutionService;
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

    public MainWindowViewModel(ICodeExecutionService codeExecutionService)
    {
        _codeExecutionService = codeExecutionService;
        ChangeView(ViewType.CodePage);
    }

    private void ChangeView(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.CodePage:
                CurrentViewModel = new CodePageViewModel(_codeExecutionService);
                break;
            default:
                throw new ArgumentException("invalid view type!");
        }

        CurrentViewModel.ViewChanged += ChangeView;
    }
}