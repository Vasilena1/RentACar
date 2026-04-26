using RentACar.Blazor.Components;
using RentACar.Blazor.Services;
using RentACar.Model.Data;

var builder = WebApplication.CreateBuilder(args);

// ── Blazor ────────────────────────────────────────────────────────────────
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

// ── DatabaseProviderService — единственият source of truth ───────────────
DbContextFactory.Provider = "Sqlite"; // стартово като WPF
builder.Services.AddSingleton<DatabaseProviderService>();

// ──────────────────────────────────────────────────────────────────────────
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