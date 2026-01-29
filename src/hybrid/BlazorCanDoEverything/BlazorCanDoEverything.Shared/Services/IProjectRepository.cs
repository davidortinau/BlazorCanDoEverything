using BlazorCanDoEverything.Shared.Models;

namespace BlazorCanDoEverything.Shared.Services;

public interface IProjectRepository
{
    Task<List<Project>> ListAsync();
    Task<Project?> GetAsync(int id);
    Task<int> SaveItemAsync(Project item);
    Task<int> DeleteItemAsync(Project item);
}
