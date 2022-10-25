using Project_Sem2_WD07_NickVn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("NickVn_Project");
builder.Services.AddDbContext<NickVn_ProjectContext>(options => options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb")));

// UseUrls - change IpAdress and port to listen
// builder.WebHost.UseUrls().UseKestrel(serverOptions =>
// {
//     serverOptions.ListenAnyIP(80);
//     serverOptions.ListenAnyIP(443, listenOptions => listenOptions.UseHttps());
// });

builder.WebHost.UseUrls().UseKestrel();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Handle error 404
app.Use(async (context, next) =>
{
    await next();
    var statusCode = context.Response.StatusCode;
    if (statusCode == 400 || statusCode == 401 || statusCode == 404)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});

// app.UseStatusCodePages();

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();