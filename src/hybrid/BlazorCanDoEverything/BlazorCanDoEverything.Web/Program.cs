using BlazorCanDoEverything.Web.Components;
using BlazorCanDoEverything.Shared.Services;
using BlazorCanDoEverything.Web.Services;
using BlazorCanDoEverything.Web.Data;
using Material.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Material.Blazor
builder.Services.AddMBServices();

// Add device-specific services used by the BlazorCanDoEverything.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// Add data repositories (in-memory for web)
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddSingleton<ITagRepository, InMemoryTagRepository>();
builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
builder.Services.AddSingleton<IProjectRepository, InMemoryProjectRepository>();
builder.Services.AddSingleton<SeedDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(
        typeof(BlazorCanDoEverything.Shared._Imports).Assembly);

app.Run();
