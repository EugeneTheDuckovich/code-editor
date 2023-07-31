using System;
using System.Windows.Controls;
using CodeEditor.Mvvm;
using CodeEditor.ViewModels.Abstract;

namespace CodeEditor.ViewModels;

public class MainWindowViewModel : NotifyPropertyChanged
{
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

    public MainWindowViewModel()
    {
        ChangeView(ViewType.CodePage);
    }

    private void ChangeView(ViewType nextView)
    {
        switch (nextView)
        {
            case ViewType.CodePage:
                CurrentViewModel = new CodePageViewModel();
                break;
            default:
                throw new ArgumentException("invalid view!");
        }
    }
}