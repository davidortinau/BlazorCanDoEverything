# BlazorCanDoEverything

A task and project management application demonstrating two parallel implementations using .NET MAUI:

- **Native MAUI** (`src/native/`) - Built with XAML and the MVVM pattern
- **Blazor Hybrid** (`src/hybrid/`) - Built with Blazor components, running on MAUI and web

Both implementations share the same functionality: managing projects, tasks, categories, and tags with a modern Fluent UI design.

## Project Structure

```
src/
├── native/                     # Native .NET MAUI app (XAML/MVVM)
│   ├── Pages/                  # XAML ContentPages
│   ├── PageModels/             # ViewModels using CommunityToolkit.Mvvm
│   ├── Models/                 # Domain entities
│   └── Data/                   # SQLite repositories
│
└── hybrid/                     # Blazor Hybrid solution
    └── BlazorCanDoEverything/
        ├── BlazorCanDoEverything.Shared/   # Shared Razor components & models
        ├── BlazorCanDoEverything/          # MAUI host project
        └── BlazorCanDoEverything.Web/      # ASP.NET Core web host
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- For iOS/Mac development: Xcode (macOS only)
- For Android development: Android SDK

### Running the Native App

```bash
# Mac Catalyst
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-maccatalyst
open src/native/bin/Debug/net10.0-maccatalyst/maccatalyst-arm64/BlazorCanDoEverything.app

# iOS Simulator
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-ios

# Android
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-android
```

### Running the Blazor Hybrid App

#### Web

```bash
cd src/hybrid/BlazorCanDoEverything
dotnet watch run --project BlazorCanDoEverything.Web
# Open http://localhost:5189
```

#### Mac Catalyst

```bash
cd src/hybrid/BlazorCanDoEverything
dotnet build BlazorCanDoEverything/BlazorCanDoEverything.csproj -f net10.0-maccatalyst
open BlazorCanDoEverything/bin/Debug/net10.0-maccatalyst/maccatalyst-arm64/BlazorCanDoEverything.app
```

## Features

- Dashboard with task overview and category chart
- Project management with tags and categories
- Task creation and completion tracking
- Light/dark theme toggle with system preference detection
- Responsive design for desktop and mobile

## Architecture

### Native App

Uses the MVVM pattern with:
- **CommunityToolkit.Mvvm** for `[ObservableProperty]` and `[RelayCommand]` source generators
- **Shell navigation** with dependency injection
- **SQLite** for local data persistence via Microsoft.Data.Sqlite
- **Syncfusion.Maui.Toolkit** for charts and UI controls

### Blazor Hybrid App

Uses a three-project architecture for maximum code sharing:
- **Shared project** contains all Razor components, models, and service interfaces
- **MAUI project** hosts Blazor in a native app via BlazorWebView
- **Web project** hosts Blazor on ASP.NET Core for browser access

UI components use [Fluent UI Blazor](https://www.fluentui-blazor.net/) for consistent styling across platforms.

## Documentation

- [.NET MAUI Documentation](https://learn.microsoft.com/dotnet/maui/)
- [.NET MAUI Product Page](https://dotnet.microsoft.com/apps/maui)
- [Blazor Hybrid Documentation](https://learn.microsoft.com/aspnet/core/blazor/hybrid/)
- [Fluent UI Blazor Components](https://www.fluentui-blazor.net/)

## License

This project is provided as a sample application for learning purposes.
