# BlazorCanDoEverything

A sample task and project management application built with [.NET MAUI](https://learn.microsoft.com/dotnet/maui/what-is-maui). This repository demonstrates two parallel implementations of the same application, showcasing different approaches to building cross-platform apps with .NET:

- **Native MAUI** - Built with XAML and the Model-View-ViewModel (MVVM) pattern
- **Blazor Hybrid** - Built with Razor components that run on MAUI and the web

Both implementations deliver the same user experience: a productivity app for managing projects, tasks, categories, and tags with a modern design that adapts to light and dark themes.

## Why Two Implementations?

This repository serves as a learning resource for developers evaluating .NET MAUI. By comparing the native and Blazor Hybrid approaches side by side, you can:

- Understand the trade-offs between native XAML and web-based UI
- See how code sharing works in each paradigm
- Learn patterns and practices for real-world .NET MAUI applications
- Make informed decisions about which approach fits your team and project

## Features

- **Dashboard** - Overview of tasks with visual category breakdown
- **Project Management** - Create and organize projects with categories and tags
- **Task Tracking** - Add tasks to projects, mark them complete, and clean up finished work
- **Theme Support** - Light and dark mode with automatic system preference detection
- **Responsive Layout** - Adapts to phone, tablet, and desktop screen sizes
- **Pull to Refresh** - Mobile-friendly gesture to reload data
- **Local Storage** - SQLite database for offline-capable data persistence

## Project Structure

```
src/
├── native/                     # Native .NET MAUI app
│   ├── Pages/                  # XAML ContentPages and Controls
│   ├── PageModels/             # ViewModels (CommunityToolkit.Mvvm)
│   ├── Models/                 # Domain entities
│   ├── Data/                   # SQLite repositories
│   └── Services/               # Application services
│
└── hybrid/
    └── BlazorCanDoEverything/
        ├── BlazorCanDoEverything.Shared/   # Shared Razor components and models
        ├── BlazorCanDoEverything/          # .NET MAUI host (BlazorWebView)
        └── BlazorCanDoEverything.Web/      # ASP.NET Core web host
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- For iOS and Mac development: [Xcode](https://developer.apple.com/xcode/) (macOS only)
- For Android development: [Android SDK](https://learn.microsoft.com/dotnet/maui/android/emulator/device-manager) via Visual Studio or command line

For detailed setup instructions, see [Install .NET MAUI](https://learn.microsoft.com/dotnet/maui/get-started/installation).

### Running the Native App

The native app targets Android, iOS, Mac Catalyst, and Windows.

```bash
# Mac Catalyst
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-maccatalyst
open src/native/bin/Debug/net10.0-maccatalyst/maccatalyst-arm64/BlazorCanDoEverything.app

# iOS Simulator
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-ios -t:Run

# Android Emulator
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-android -t:Run

# Windows
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-windows10.0.19041.0 -t:Run
```

### Running the Blazor Hybrid App

The Blazor Hybrid app can run as a native mobile/desktop app or in a web browser.

#### Web Browser

```bash
cd src/hybrid/BlazorCanDoEverything
dotnet watch run --project BlazorCanDoEverything.Web
```

Then open http://localhost:5189 in your browser.

#### Mac Catalyst

```bash
cd src/hybrid/BlazorCanDoEverything
dotnet build BlazorCanDoEverything/BlazorCanDoEverything.csproj -f net10.0-maccatalyst
open BlazorCanDoEverything/bin/Debug/net10.0-maccatalyst/maccatalyst-arm64/BlazorCanDoEverything.app
```

#### iOS and Android

```bash
cd src/hybrid/BlazorCanDoEverything
dotnet build BlazorCanDoEverything/BlazorCanDoEverything.csproj -f net10.0-ios -t:Run
dotnet build BlazorCanDoEverything/BlazorCanDoEverything.csproj -f net10.0-android -t:Run
```

## Architecture

### Native MAUI App

The native implementation follows the [MVVM pattern](https://learn.microsoft.com/dotnet/maui/xaml/fundamentals/mvvm) with these key technologies:

| Component | Technology | Purpose |
|-----------|------------|---------|
| UI | [XAML](https://learn.microsoft.com/dotnet/maui/xaml/) | Declarative UI with compiled bindings |
| ViewModels | [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) | Source generators for `[ObservableProperty]` and `[RelayCommand]` |
| Navigation | [.NET MAUI Shell](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/) | URI-based navigation with dependency injection |
| Data | [Microsoft.Data.Sqlite](https://learn.microsoft.com/dotnet/standard/data/sqlite/) | Lightweight local database |
| Controls | [Syncfusion MAUI Toolkit](https://help.syncfusion.com/maui-toolkit/introduction/overview) | Charts, pull-to-refresh, and input layouts |

Key patterns demonstrated:

- **Compiled Bindings** - Using `x:DataType` for type-safe, performant data binding
- **EventToCommandBehavior** - Converting page lifecycle events to commands via [.NET MAUI Community Toolkit](https://learn.microsoft.com/dotnet/communitytoolkit/maui/)
- **Repository Pattern** - Abstracting data access with async initialization
- **Shell Route Registration** - `AddTransientWithShellRoute<TPage, TViewModel>()` for navigation with DI

### Blazor Hybrid App

The Blazor Hybrid implementation maximizes code sharing between native and web using a [three-project architecture](https://learn.microsoft.com/aspnet/core/blazor/hybrid/):

| Project | Purpose |
|---------|---------|
| **Shared** | Razor components, models, and service interfaces (targets `net10.0`) |
| **MAUI** | Native host using [BlazorWebView](https://learn.microsoft.com/dotnet/maui/user-interface/controls/blazorwebview) |
| **Web** | ASP.NET Core host for browser deployment |

The shared project contains all UI components written as [Razor components](https://learn.microsoft.com/aspnet/core/blazor/components/), which render identically on mobile, desktop, and web. Platform-specific behavior is abstracted through interfaces like `IFormFactor`.

UI styling uses [Material.Blazor](https://material-blazor.com/) for Material Design components.

## Key Concepts Demonstrated

### For Native MAUI Development

- [Data binding with compiled bindings](https://learn.microsoft.com/dotnet/maui/fundamentals/data-binding/compiled-bindings)
- [Shell navigation and routing](https://learn.microsoft.com/dotnet/maui/fundamentals/shell/navigation)
- [Dependency injection](https://learn.microsoft.com/dotnet/maui/fundamentals/dependency-injection)
- [Custom controls and templates](https://learn.microsoft.com/dotnet/maui/fundamentals/controltemplate)
- [App theming with AppThemeBinding](https://learn.microsoft.com/dotnet/maui/user-interface/system-theme-changes)
- [Responsive layouts with OnIdiom](https://learn.microsoft.com/dotnet/maui/xaml/fundamentals/resource-dictionaries#provide-platform-specific-or-device-specific-styling)

### For Blazor Hybrid Development

- [Building a Blazor Hybrid app](https://learn.microsoft.com/aspnet/core/blazor/hybrid/tutorials/maui)
- [Code sharing between web and native](https://learn.microsoft.com/aspnet/core/blazor/hybrid/reuse-razor-components)
- [Platform-specific services via dependency injection](https://learn.microsoft.com/aspnet/core/blazor/fundamentals/dependency-injection)
- [Blazor component lifecycle](https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle)

## Learn More

### Official Documentation

- [.NET MAUI Documentation](https://learn.microsoft.com/dotnet/maui/) - Comprehensive guide to building native apps
- [Blazor Hybrid Documentation](https://learn.microsoft.com/aspnet/core/blazor/hybrid/) - Build hybrid apps with Blazor
- [.NET MAUI Samples](https://learn.microsoft.com/samples/browse/?products=dotnet-maui) - Official sample applications

### Community Resources

- [.NET MAUI Community Toolkit](https://learn.microsoft.com/dotnet/communitytoolkit/maui/) - Behaviors, converters, and controls
- [MVVM Toolkit](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) - Source generators for MVVM

### Tools and Extensions

- [.NET MAUI in Visual Studio](https://learn.microsoft.com/dotnet/maui/get-started/first-app)
- [.NET MAUI in VS Code](https://learn.microsoft.com/dotnet/maui/get-started/installation?tabs=visual-studio-code)

## Contributing

This is a sample application intended for learning. Issues and pull requests that improve the educational value of the samples are welcome.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
