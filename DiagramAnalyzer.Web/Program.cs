using DiagramAnalyzer.Core.Configuration;
using DiagramAnalyzer.Core.Services;
using DiagramAnalyzer.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<AzureVisionSettings>(
    builder.Configuration.GetSection("AzureVision"));
builder.Services.Configure<AzureOpenAISettings>(
    builder.Configuration.GetSection("AzureOpenAI"));

builder.Services.AddScoped<IAzureVisionService, AzureVisionService>();
builder.Services.AddScoped<IGptVisionService, GptVisionService>();
builder.Services.AddScoped<IDiagramProcessorService, DiagramProcessorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();