using BlazorCanDoEverything.Shared.Models;
using BlazorCanDoEverything.Shared.Services;

namespace BlazorCanDoEverything.Web.Data;

public class InMemoryProjectRepository : IProjectRepository
{
    private readonly List<Project> _projects = [];
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;
    private int _nextId = 1;

    public InMemoryProjectRepository(ITaskRepository taskRepository, ITagRepository tagRepository)
    {
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
    }

    public Task<List<Project>> ListAsync()
    {
        // Load tasks and tags for each project
        foreach (var project in _projects)
        {
            project.Tasks = _taskRepository.ListAsync(project.ID).Result;
            project.Tags = _tagRepository.ListAsync(project.ID).Result;
        }
        return Task.FromResult(_projects.ToList());
    }

    public Task<Project?> GetAsync(int id)
    {
        var project = _projects.FirstOrDefault(p => p.ID == id);
        if (project != null)
        {
            project.Tasks = _taskRepository.ListAsync(project.ID).Result;
            project.Tags = _tagRepository.ListAsync(project.ID).Result;
        }
        return Task.FromResult(project);
    }

    public Task<int> SaveItemAsync(Project item)
    {
        if (item.ID == 0)
        {
            item.ID = _nextId++;
            _projects.Add(item);
        }
        else
        {
            var index = _projects.FindIndex(p => p.ID == item.ID);
            if (index >= 0)
            {
                _projects[index] = item;
            }
        }
        return Task.FromResult(item.ID);
    }

    public Task<int> DeleteItemAsync(Project item)
    {
        var removed = _projects.RemoveAll(p => p.ID == item.ID);
        return Task.FromResult(removed);
    }
}
