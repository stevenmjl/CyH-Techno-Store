using CyH_Techno_Store.Components;
using CyH_Techno_Store.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CyH_Techno_Store.Services;
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

builder.Services.AddScoped<UserAccountsService>();
builder.Services.AddScoped<ProductossService>();
builder.Services.AddScoped<FacturassService>();
builder.Services.AddScoped<DetalleFacturasService>();
// Servicios para el apartado del Login
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
