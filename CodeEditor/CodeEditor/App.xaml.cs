using System.Windows;
using CodeEditor.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>services.ConfigureServices())
            .Build();
    }
    
    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host!.StartAsync();
        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
        base.OnStartup(e);
    }
 
    protected override async void OnExit(ExitEventArgs e)
    {
        await _host!.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }
}