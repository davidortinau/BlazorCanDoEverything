using System.Text.Json.Serialization;

namespace BlazorCanDoEverything.Shared.Models;

public class Category
{
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Color { get; set; } = "#FF0000";

    public override string ToString() => Title;
}
