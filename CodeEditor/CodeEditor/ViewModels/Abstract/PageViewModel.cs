using System;
using System.Linq;
using CodeEditor.Mvvm;

namespace CodeEditor.ViewModels.Abstract;

public enum ViewType
{
    CodePage,
}

public class PageViewModel : NotifyPropertyChanged
{
    private Action<ViewType> _viewChanged;
    
    public event Action<ViewType>  ViewChanged
    {
        add => _viewChanged += value;
        remove
        {
            if (!_viewChanged.GetInvocationList().Contains(value))
                _viewChanged -= value;
        }
    }
}