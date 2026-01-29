using BlazorCanDoEverything.Shared.Models;

namespace BlazorCanDoEverything.Shared.Services;

public interface ITaskRepository
{
    Task<List<ProjectTask>> ListAsync();
    Task<List<ProjectTask>> ListAsync(int projectId);
    Task<ProjectTask?> GetAsync(int id);
    Task<int> SaveItemAsync(ProjectTask item);
    Task<int> DeleteItemAsync(ProjectTask item);
}
