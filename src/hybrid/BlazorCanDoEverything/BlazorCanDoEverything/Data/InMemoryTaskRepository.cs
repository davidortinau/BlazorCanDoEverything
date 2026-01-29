using BlazorCanDoEverything.Shared.Models;
using BlazorCanDoEverything.Shared.Services;

namespace BlazorCanDoEverything.Data;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<ProjectTask> _tasks = [];
    private int _nextId = 1;

    public Task<List<ProjectTask>> ListAsync()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task<List<ProjectTask>> ListAsync(int projectId)
    {
        return Task.FromResult(_tasks.Where(t => t.ProjectID == projectId).ToList());
    }

    public Task<ProjectTask?> GetAsync(int id)
    {
        return Task.FromResult(_tasks.FirstOrDefault(t => t.ID == id));
    }

    public Task<int> SaveItemAsync(ProjectTask item)
    {
        if (item.ID == 0)
        {
            item.ID = _nextId++;
            _tasks.Add(item);
        }
        else
        {
            var index = _tasks.FindIndex(t => t.ID == item.ID);
            if (index >= 0)
            {
                _tasks[index] = item;
            }
        }
        return Task.FromResult(item.ID);
    }

    public Task<int> DeleteItemAsync(ProjectTask item)
    {
        var removed = _tasks.RemoveAll(t => t.ID == item.ID);
        return Task.FromResult(removed);
    }
}
