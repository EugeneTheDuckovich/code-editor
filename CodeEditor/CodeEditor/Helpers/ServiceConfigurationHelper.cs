using CodeEditor.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CodeEditor.Helpers;

public static class ServiceConfigurationHelper
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
    }
}