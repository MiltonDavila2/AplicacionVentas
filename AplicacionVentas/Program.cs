using AplicacionVentas.Infraestructura;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ContextoDatos>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConexionBase"]);
});


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>

{
    options.IdleTimeout=TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Productos}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "productos",
    pattern: "/productos/{categorySlug?}",
    defaults: new { controller = "Productos", action = "Index" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



var context= app.Services.CreateScope().ServiceProvider.GetRequiredService<ContextoDatos>();
SeedData.SeedDatabase(context);

app.Run();
