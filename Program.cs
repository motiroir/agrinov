using AgriNov.Models;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

Env.Load();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseStaticFiles();

using( ServiceUserAccount sUA = new ServiceUserAccount()){
    sUA.CreateDeleteDatabase();
    sUA.InitializeTable();
}

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();