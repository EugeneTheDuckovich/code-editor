using System;
using System.Linq;

namespace CodeEditor.ViewModels.Abstract;

public enum ViewType
{
    CodePage,
}

public class PageViewModel
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