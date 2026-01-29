using CommunityToolkit.Mvvm.Input;
using BlazorCanDoEverything.Models;

namespace BlazorCanDoEverything.PageModels;

public interface IProjectTaskPageModel
{
	IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
	bool IsBusy { get; }
}