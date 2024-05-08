using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;
using ValorVault.Services.UserService;
using Serilog;

//public class Program
//{
//    public static void Main(string[] args)
//    {
var builder = WebApplication.CreateBuilder(args);

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateLogger();

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddSerilog();
//});


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SoldierInfoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SoldierInfoDatabase")));

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<SoldierInfoDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISoldierInfoService, SoldierInfoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registration}/{action=Register}/{id?}");

app.Run();
//    }
//}