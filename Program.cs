using CyH_Techno_Store.Components;
using CyH_Techno_Store.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Obtenermos el ConStr para agregarlo al contexto
var ConStr = builder.Configuration.GetConnectionString("SqlConStr");

// Agregamos el contexto al builder con el ConStr
builder.Services.AddDbContextFactory<Contexto>(c => c.UseSqlServer(ConStr));

// Inyeción de servicios a usar
builder.Services.AddBlazorBootstrap();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
