using BlazorCanDoEverything.Shared.Models;

namespace BlazorCanDoEverything.Shared.Services;

public class SeedDataService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SeedDataService(
        IProjectRepository projectRepository, 
        ITaskRepository taskRepository, 
        ITagRepository tagRepository, 
        ICategoryRepository categoryRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task LoadSeedDataAsync()
    {
        var seedData = GetSeedData();

        foreach (var project in seedData)
        {
            if (project.Category != null)
            {
                await _categoryRepository.SaveItemAsync(project.Category);
                project.CategoryID = project.Category.ID;
            }

            await _projectRepository.SaveItemAsync(project);

            if (project.Tasks != null)
            {
                foreach (var task in project.Tasks)
                {
                    task.ProjectID = project.ID;
                    await _taskRepository.SaveItemAsync(task);
                }
            }

            if (project.Tags != null)
            {
                foreach (var tag in project.Tags)
                {
                    await _tagRepository.SaveItemAsync(tag, project.ID);
                }
            }
        }
    }

    private static List<Project> GetSeedData() =>
    [
        new Project
        {
            Name = "Balance",
            Description = "Improve work-life balance.",
            Icon = "\uea28",
            Category = new Category { Title = "work", Color = "#3068df" },
            Tags = [new Tag { Title = "work", Color = "#3068df" }],
            Tasks =
            [
                new ProjectTask { Title = "Survey Employees", IsCompleted = false },
                new ProjectTask { Title = "Analyze Survey Results", IsCompleted = false },
                new ProjectTask { Title = "Develop Action Plan", IsCompleted = false }
            ]
        },
        new Project
        {
            Name = "Personal",
            Description = "Learn to speak another language.",
            Icon = "\uf8fe",
            Category = new Category { Title = "education", Color = "#8800FF" },
            Tags = [new Tag { Title = "personal", Color = "#FF4500" }],
            Tasks =
            [
                new ProjectTask { Title = "Read a Book", IsCompleted = false },
                new ProjectTask { Title = "Attend a Workshop", IsCompleted = false },
                new ProjectTask { Title = "Practice a Hobby", IsCompleted = false }
            ]
        },
        new Project
        {
            Name = "Fitness",
            Description = "Promote health and fitness activities",
            Icon = "\uf837",
            Category = new Category { Title = "self", Color = "#FF3300" },
            Tags = [new Tag { Title = "health", Color = "#32CD32" }],
            Tasks =
            [
                new ProjectTask { Title = "Morning Yoga", IsCompleted = false },
                new ProjectTask { Title = "Evening Run", IsCompleted = false },
                new ProjectTask { Title = "Healthy Cooking Class", IsCompleted = false }
            ]
        },
        new Project
        {
            Name = "Family and Friends",
            Description = "Strengthen relationships with family and friends.",
            Icon = "\uf5a9",
            Category = new Category { Title = "relationships", Color = "#FF9900" },
            Tags =
            [
                new Tag { Title = "family", Color = "#1E90FF" },
                new Tag { Title = "friends", Color = "#FF69B4" }
            ],
            Tasks =
            [
                new ProjectTask { Title = "Plan a Family Reunion", IsCompleted = false },
                new ProjectTask { Title = "Organize a Friends' Get-together", IsCompleted = false },
                new ProjectTask { Title = "Weekly Phone Calls", IsCompleted = false }
            ]
        }
    ];
}
