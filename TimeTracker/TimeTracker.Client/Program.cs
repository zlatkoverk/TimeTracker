using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using TimeTracker.Client.Services;

namespace TimeTracker.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<AppState>();
                services.AddSingleton<RegisterService>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<Main>("app");
        }
    }
}
