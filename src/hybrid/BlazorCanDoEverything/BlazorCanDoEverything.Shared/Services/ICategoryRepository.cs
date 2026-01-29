using BlazorCanDoEverything.Shared.Models;

namespace BlazorCanDoEverything.Shared.Services;

public interface ICategoryRepository
{
    Task<List<Category>> ListAsync();
    Task<Category?> GetAsync(int id);
    Task<int> SaveItemAsync(Category item);
    Task<int> DeleteItemAsync(Category item);
}
