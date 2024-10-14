using AgriNov.Models;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Login/Login");

var app = builder.Build();

Env.Load();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

using( ServiceUserAccount sUA = new ServiceUserAccount()){
    sUA.CreateDeleteDatabase();
    sUA.InitializeTable();
}

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();