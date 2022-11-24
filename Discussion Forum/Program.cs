using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.DBConnection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBforumConnection") ?? throw new InvalidOperationException("Connection string 'DBforumConnection' not found.");

builder.Services.AddDbContext<DBforum>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DBforum>();

builder.Services.AddControllersWithViews();
builder.Services.ConnectToSqlServer(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
