
using CyH_Techno_Store.Components;
using CyH_Techno_Store.DAL;
using CyH_Techno_Store.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<ProductosService>();
builder.Services.AddScoped<FacturasService>();
builder.Services.AddScoped<DetalleFacturasService>();
builder.Services.AddScoped<FacturaAdminsService>();
builder.Services.AddScoped<DetalleFacturaAdminsService>();
builder.Services.AddScoped<CategoriasService>();
builder.Services.AddScoped<ProveedoresService>();
builder.Services.AddScoped<CarritoService>();

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
