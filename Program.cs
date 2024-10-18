using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProductService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => 
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/Login";
    }
    );


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

using (ServiceUserAccount sUA = new ServiceUserAccount())
{
    sUA.CreateDeleteDatabase();
    sUA.InitializeTable();
}
using(IServiceUser sU = new ServiceUser())
{
    sU.InitializeTable();
}
using(IServiceSupplier sP = new ServiceSupplier())
{
    sP.InitializeTable();
}
using(IServiceCorporateUser sCU = new ServiceCorporateUser())
{
    sCU.InitializeTable();
}
using (ServiceProduction sP = new ServiceProduction())
{
    sP.Initializetable();
}
using (ServiceActivity sA = new ServiceActivity())
{
    sA.InitializeTable();
}
using (ProductService pS = new ProductService())
{
    pS.InitializeTable();
}
   
    
    
    
    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();