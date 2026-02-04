namespace BlazorCanDoEverything.Shared.Services;

public interface IFormFactor
{
    public string GetFormFactor();
    public string GetPlatform();
    public bool IsMobile();
}
