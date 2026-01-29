using Microsoft.Extensions.Logging;
using Material.Blazor;
using BlazorCanDoEverything.Shared.Services;
using BlazorCanDoEverything.Services;
using BlazorCanDoEverything.Data;

namespace BlazorCanDoEverything;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the BlazorCanDoEverything.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

        // Add Material.Blazor
        builder.Services.AddMBServices();

        // Add data repositories (in-memory for now, can switch to SQLite)
        builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
        builder.Services.AddSingleton<ITagRepository, InMemoryTagRepository>();
        builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
        builder.Services.AddSingleton<IProjectRepository, InMemoryProjectRepository>();
        builder.Services.AddSingleton<SeedDataService>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
