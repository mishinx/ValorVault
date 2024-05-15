using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;
using ValorVault.Services;
using ValorVault.Services.SourceService;
using ValorVault.Services.UserService;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<SoldierInfoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SoldierInfoDatabase")));

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<SoldierInfoDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISoldierInfoService, SoldierInfoService>();
builder.Services.AddScoped<ISourceService, SourceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/ProfileView/Error");
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