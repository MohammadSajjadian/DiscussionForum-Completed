using Data.Context;
using Data.Entities;
using Extension.DBConnection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Email;
using Extension.LockedOut;
using Service.ResizeImage;
using Extension.AccessLevelsPathEx;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBforumConnection") ?? throw new InvalidOperationException("Connection string 'DBforumConnection' not found.");

builder.Services.AddDbContext<DBforum>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DBforum>();

builder.Services.AddControllersWithViews();
builder.Services.ConnectToSqlServer(builder.Configuration);
builder.Services.LockedOutConfiguration();
builder.Services.AccessLevelPathConfiguration();

builder.Services.AddAuthentication();

builder.Services.AddSingleton<IEmail, Email>();
builder.Services.AddSingleton<IResize, Resize>();

var app = builder.Build();

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
