using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Email;
using Extension.LockedOut;
using Service.ResizeImage;
using Extension.AccessLevelsPathEx;
using Extension.RoleEx;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBforumConnection") ?? throw new InvalidOperationException("Connection string 'DBforumConnection' not found.");

builder.Services.AddDbContext<DBforum>(options => options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DBforum>();

builder.Services.AddControllersWithViews();
builder.Services.LockedOutConfiguration();
builder.Services.AccessLevelPathConfiguration();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options => { options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin")); });

builder.Services.AddSingleton<IEmail, Email>();
builder.Services.AddSingleton<IResize, Resize>();

var app = builder.Build();

// Dependency Injection for userManager and roleManager.
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await builder.Services.CreateRole(userManager, roleManager);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
