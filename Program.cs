using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// Connection info stored in appsettings.json
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Register the DataContext service
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration["Data:Blog:ConnectionString"]));
var app = builder.Build();
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(configuration["Data:AppIdentity:ConnectionString"]));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

// "ConnectionString": "Server=bitsql.wctc.edu;Database=Blogs_17_SFW;User ID=swiniarski2;Password=000449416"
