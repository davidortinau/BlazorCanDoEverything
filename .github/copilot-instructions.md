# Copilot Instructions for BlazorCanDoEverything

## Project Overview

This repository contains two parallel implementations of a task/project management app demonstrating different .NET MAUI UI approaches:

- **`src/native/`** - Native .NET MAUI app using XAML/MVVM
- **`src/hybrid/`** - Blazor Hybrid app with code sharing between MAUI and web

## Verification Requirements

**MANDATORY**: All changes must be verified before declaring work complete. Never say "should work" or "refresh to see changes" without first confirming the fix yourself.

### Verification Methods

1. **Unit Tests**: Run existing tests with `dotnet test`. Add tests for new functionality.

2. **UI Tests - Web (Playwright)**:
   ```bash
   # Playwright is installed via pip3
   python3 -c "from playwright.sync_api import sync_playwright; ..."
   ```
   - Start web server: `dotnet watch run --project BlazorCanDoEverything.Web`
   - Write Playwright scripts to verify UI behavior
   - Check element visibility, colors, text content, interactions

3. **UI Tests - MAUI (Appium)**:
   - Use `appium-automation` skill for iOS/Android/Mac Catalyst
   - Verify visual elements and interactions on device

4. **Visual Verification**:
   - Use `curl` to fetch rendered HTML and grep for expected content
   - Check CSS properties via Playwright's `page.evaluate()`

5. **Log Inspection**:
   - Use `maui-diagnostics` skill for runtime logs
   - Add Console.WriteLine or ILogger statements to trace execution

### Verification Workflow

```
1. Understand the problem (inspect current code/behavior)
2. Create a plan (present to user for approval in PLAN mode)
3. Implement changes (only after plan approval)
4. Verify the fix:
   - Run automated tests if available
   - Use Playwright/Appium to confirm UI changes
   - Check logs for expected behavior
5. Iterate if verification fails
6. Only report success after verification passes
```

### Never Do

- Assume changes work without verification
- Tell user to "refresh and check" without checking yourself first
- Skip the verification step due to time or complexity
- Declare success based on "the code looks correct"

## Build Commands

```bash
# Native MAUI app
dotnet build src/native/BlazorCanDoEverything.csproj

# Hybrid solution (all projects)
dotnet build src/hybrid/BlazorCanDoEverything/BlazorCanDoEverything.sln

# Run web project (hybrid)
dotnet run --project src/hybrid/BlazorCanDoEverything/BlazorCanDoEverything.Web

# Target specific platform (native or hybrid MAUI)
dotnet build -f net10.0-ios
dotnet build -f net10.0-android
dotnet build -f net10.0-maccatalyst
```

## Architecture

### Native MAUI App (`src/native/`)

Uses MVVM pattern with CommunityToolkit.Mvvm:

| Layer | Location | Description |
|-------|----------|-------------|
| Views | `Pages/` | XAML ContentPages with `x:DataType` compiled bindings |
| ViewModels | `PageModels/` | ObservableObject classes using `[ObservableProperty]` and `[RelayCommand]` |
| Data | `Data/` | Repository pattern with raw SQLite (Microsoft.Data.Sqlite) |
| Models | `Models/` | Domain entities |
| Controls | `Pages/Controls/` | Reusable XAML components |

Shell navigation with `AddTransientWithShellRoute<Page, PageModel>()` for routing.

### Blazor Hybrid App (`src/hybrid/`)

Three-project structure for code sharing:

| Project | Purpose |
|---------|---------|
| `BlazorCanDoEverything.Shared` | Razor components, shared UI, `IFormFactor` abstraction |
| `BlazorCanDoEverything` | MAUI host with BlazorWebView |
| `BlazorCanDoEverything.Web` | ASP.NET Core Blazor Server host |

Platform-specific services implement `IFormFactor` interface registered via DI.

## Key Conventions

### XAML & Bindings (Native)
- Always use `x:DataType` for compiled bindings
- Use `EventToCommandBehavior` from CommunityToolkit.Maui for lifecycle events
- Reference ancestor BindingContext with `Source={RelativeSource AncestorType={x:Type vm:PageModelType}}`

### ViewModels (Native)
- Inherit from `ObservableObject`
- Use `[ObservableProperty]` for bindable properties (generates `OnPropertyChanged`)
- Use `[RelayCommand]` for commands (generates `*Command` properties)
- Implement `IProjectTaskPageModel` interface for pages that display tasks

### DI Registration (Native)
```csharp
// Singleton services
builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<PageModel>();

// Transient with Shell routing
builder.Services.AddTransientWithShellRoute<DetailPage, DetailPageModel>("route");
```

### Data Layer (Native)
- Repositories use async initialization pattern with `_hasBeenInitialized` flag
- Each repository manages its own table creation
- Use parameterized queries (`@paramName`) to prevent SQL injection

### UI Components
- Syncfusion.Maui.Toolkit for charts, pull-to-refresh, segmented control, text input layouts
- FluentUI icon font (`FluentSystemIcons-Regular.ttf`) referenced via `Fonts.FluentUI` static class
- Fluent Design typography styles: `Title1`, `Title2`, `Body1`, `Caption1`, etc.

### Resources & Theming
- App-wide styles in `Resources/Styles/AppStyles.xaml`
- Use `AppThemeBinding` for light/dark mode support
- Responsive sizing with `OnIdiom` (Default vs Desktop)

## Appium UI Automation

Use Appium for on-device UI testing and validation. The `appium-automation` skill is available.

### Prerequisites

```bash
# Install Appium and drivers
npm install -g appium
appium driver install xcuitest      # iOS
appium driver install uiautomator2  # Android
appium driver install mac2          # Mac Catalyst

# Python dependencies
pip install Appium-Python-Client selenium
```

### App IDs

| App | Bundle/Package ID |
|-----|-------------------|
| Native MAUI | `com.companyname.blazorcandoeverything` |
| Hybrid MAUI | `com.companyname.blazorcandoeverything` |

### Build & Deploy for Automation

```bash
# iOS Simulator
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-ios
xcrun simctl install booted bin/Debug/net10.0-ios/iossimulator-arm64/BlazorCanDoEverything.app

# Android (MUST use EmbedAssembliesIntoApk for Appium)
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-android -p:EmbedAssembliesIntoApk=true
adb install bin/Debug/net10.0-android/com.companyname.blazorcandoeverything-Signed.apk

# Mac Catalyst
dotnet build src/native/BlazorCanDoEverything.csproj -f net10.0-maccatalyst
```

### Example Automation

```bash
# Boot simulator and start Appium
python ~/.copilot/installed-plugins/maui-copilot-plugins/appium-automation/skills/appium-automation/scripts/automate.py --boot-simulator "iPhone 16 Pro"
python ~/.copilot/installed-plugins/maui-copilot-plugins/appium-automation/skills/appium-automation/scripts/automate.py --start-appium

# List elements to find AutomationIds
python ~/.copilot/installed-plugins/maui-copilot-plugins/appium-automation/skills/appium-automation/scripts/automate.py \
  --platform ios --app-id com.companyname.blazorcandoeverything --list-elements

# Chain actions for better performance
python ~/.copilot/installed-plugins/maui-copilot-plugins/appium-automation/skills/appium-automation/scripts/automate.py \
  --platform ios --app-id com.companyname.blazorcandoeverything \
  --tap AddTaskButton --wait 1 --get-text TaskTitleEntry
```

### AutomationId Convention

Add `AutomationId` to interactive XAML elements for reliable targeting:

```xml
<Button AutomationId="AddTaskButton" Text="Add" Command="{Binding AddTaskCommand}" />
<Entry AutomationId="TaskTitleEntry" Placeholder="Task name" />
```

## Runtime Diagnostics

Use the `maui-diagnostics` skill for capturing logs and debugging running apps.

### Log Capture

```bash
# Stream logs from iOS Simulator
python ~/.copilot/skills/maui-diagnostics/scripts/maui_logs.py \
  --platform ios --app-id com.companyname.blazorcandoeverything --stream

# Android logs
python ~/.copilot/skills/maui-diagnostics/scripts/maui_logs.py \
  --platform android --app-id com.companyname.blazorcandoeverything --stream

# Filter for errors
python ~/.copilot/skills/maui-diagnostics/scripts/maui_logs.py \
  --platform ios --app-id com.companyname.blazorcandoeverything --level error
```

### Adding File Logger

For persistent logs, add FileLogger to MauiProgram.cs:

```bash
# Add FileLogger.cs to native project
python ~/.copilot/skills/maui-diagnostics/scripts/add_file_logger.py add src/native

# Pull logs from device
python ~/.copilot/skills/maui-diagnostics/scripts/add_file_logger.py pull \
  --platform ios --app-id com.companyname.blazorcandoeverything --output ./logs
```

### Iterative Debug Loop

Combine diagnostics with Appium for autonomous debugging:

1. Start log capture in background
2. Make code changes
3. Build and deploy: `dotnet build -f net10.0-ios`
4. Exercise UI with Appium automation
5. Check logs for errors/expected output
6. Repeat until resolved
