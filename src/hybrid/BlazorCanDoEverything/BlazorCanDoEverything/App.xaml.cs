using BlazorCanDoEverything.Shared.Services;

namespace BlazorCanDoEverything;

public partial class App : Application
{
    private readonly IFormFactor _formFactor;

    public App(IFormFactor formFactor)
    {
        InitializeComponent();
        _formFactor = formFactor;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Page mainPage = _formFactor.IsMobile() ? new AppShell() : new MainPage();
        return new Window(mainPage) { Title = "BlazorCanDoEverything" };
    }
}
