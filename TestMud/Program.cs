using TestMud.Components;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using TestMud.Models;
using TestMud.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ connect PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

var scope = app.Services.CreateScope();
var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

await userService.AddUserAsync(new User { Name = "Test User", Email = "test@email.com" });

Console.WriteLine("🔵 ทดสอบเพิ่มผู้ใช้จาก `Program.cs` แล้ว");
