using BlazorCanDoEverything.Shared.Models;

namespace BlazorCanDoEverything.Shared.Services;

public interface ITagRepository
{
    Task<List<Tag>> ListAsync();
    Task<List<Tag>> ListAsync(int projectId);
    Task<Tag?> GetAsync(int id);
    Task<int> SaveItemAsync(Tag item);
    Task<int> SaveItemAsync(Tag item, int projectId);
    Task<int> DeleteItemAsync(Tag item);
    Task<int> DeleteItemAsync(Tag item, int projectId);
}
