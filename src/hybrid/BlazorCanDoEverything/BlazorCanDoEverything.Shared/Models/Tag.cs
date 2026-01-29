using System.Text.Json.Serialization;

namespace BlazorCanDoEverything.Shared.Models;

public class Tag
{
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Color { get; set; } = "#FF0000";

    [JsonIgnore]
    public bool IsSelected { get; set; }
}
