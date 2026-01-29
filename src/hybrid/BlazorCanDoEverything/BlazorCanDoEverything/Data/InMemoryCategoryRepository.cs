using BlazorCanDoEverything.Shared.Models;
using BlazorCanDoEverything.Shared.Services;

namespace BlazorCanDoEverything.Data;

public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = [];
    private int _nextId = 1;

    public Task<List<Category>> ListAsync()
    {
        return Task.FromResult(_categories.ToList());
    }

    public Task<Category?> GetAsync(int id)
    {
        return Task.FromResult(_categories.FirstOrDefault(c => c.ID == id));
    }

    public Task<int> SaveItemAsync(Category item)
    {
        if (item.ID == 0)
        {
            item.ID = _nextId++;
            _categories.Add(item);
        }
        else
        {
            var index = _categories.FindIndex(c => c.ID == item.ID);
            if (index >= 0)
            {
                _categories[index] = item;
            }
        }
        return Task.FromResult(item.ID);
    }

    public Task<int> DeleteItemAsync(Category item)
    {
        var removed = _categories.RemoveAll(c => c.ID == item.ID);
        return Task.FromResult(removed);
    }
}
