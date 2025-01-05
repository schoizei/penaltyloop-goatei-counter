using GoateiCounter.Components;

namespace GoateiCounter;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseRouting();
        app.UseAntiforgery();
        app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true, });
        app.UseStaticFiles();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapStaticAssets();
        app.MapControllers();

        app.Run();
    }
}
