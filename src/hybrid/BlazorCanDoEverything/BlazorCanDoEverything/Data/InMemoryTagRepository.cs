using BlazorCanDoEverything.Shared.Models;
using BlazorCanDoEverything.Shared.Services;

namespace BlazorCanDoEverything.Data;

public class InMemoryTagRepository : ITagRepository
{
    private readonly List<Tag> _tags = [];
    private readonly Dictionary<int, List<int>> _projectTags = []; // projectId -> list of tagIds
    private int _nextId = 1;

    public Task<List<Tag>> ListAsync()
    {
        return Task.FromResult(_tags.ToList());
    }

    public Task<List<Tag>> ListAsync(int projectId)
    {
        if (_projectTags.TryGetValue(projectId, out var tagIds))
        {
            return Task.FromResult(_tags.Where(t => tagIds.Contains(t.ID)).ToList());
        }
        return Task.FromResult(new List<Tag>());
    }

    public Task<Tag?> GetAsync(int id)
    {
        return Task.FromResult(_tags.FirstOrDefault(t => t.ID == id));
    }

    public Task<int> SaveItemAsync(Tag item)
    {
        if (item.ID == 0)
        {
            item.ID = _nextId++;
            _tags.Add(item);
        }
        else
        {
            var index = _tags.FindIndex(t => t.ID == item.ID);
            if (index >= 0)
            {
                _tags[index] = item;
            }
        }
        return Task.FromResult(item.ID);
    }

    public Task<int> SaveItemAsync(Tag item, int projectId)
    {
        SaveItemAsync(item);
        
        if (!_projectTags.ContainsKey(projectId))
        {
            _projectTags[projectId] = [];
        }
        
        if (!_projectTags[projectId].Contains(item.ID))
        {
            _projectTags[projectId].Add(item.ID);
        }
        
        return Task.FromResult(1);
    }

    public Task<int> DeleteItemAsync(Tag item)
    {
        var removed = _tags.RemoveAll(t => t.ID == item.ID);
        // Also remove from all project associations
        foreach (var tagList in _projectTags.Values)
        {
            tagList.Remove(item.ID);
        }
        return Task.FromResult(removed);
    }

    public Task<int> DeleteItemAsync(Tag item, int projectId)
    {
        if (_projectTags.TryGetValue(projectId, out var tagIds))
        {
            tagIds.Remove(item.ID);
            return Task.FromResult(1);
        }
        return Task.FromResult(0);
    }
}
