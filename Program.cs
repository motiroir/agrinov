using AgriNov.Models;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProductService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => 
    {
        options.LoginPath = "/Login/LoginWithSignupSlide";
        options.AccessDeniedPath = "/Login/AccessDenied";
    }
    );


var app = builder.Build();

Env.Load();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
 app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (ServiceUserAccount sUA = new ServiceUserAccount())
{
    sUA.CreateDeleteDatabase();
    sUA.InitializeTable();
}
using (IServiceUser sU = new ServiceUser())
{
    sU.InitializeTable();
}
using (IServiceSupplier sP = new ServiceSupplier())
{
    sP.InitializeTable();
}
using (IServiceCorporateUser sCU = new ServiceCorporateUser())
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
using (ServiceBoxContract bC = new ServiceBoxContract())
{
    bC.InitializeTable();
}
using (ServiceBooking sB = new ServiceBooking())
{
    sB.InitializeTable();
}
using (IServiceMemberShipFee sMB = new ServiceMemberShipFee())
{
    sMB.InitializeTable();
}
using (IServiceShoppingCart sSC = new ServiceShoppingCart())
{
    sSC.InitializeTable();
}
using (IServiceOrder sO = new ServiceOrder())
{
    sO.InitializeTable();
}

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();