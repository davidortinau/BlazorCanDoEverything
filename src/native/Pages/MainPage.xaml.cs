using BlazorCanDoEverything.Models;
using BlazorCanDoEverything.PageModels;

namespace BlazorCanDoEverything.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}