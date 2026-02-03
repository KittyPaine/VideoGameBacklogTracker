using Microsoft.EntityFrameworkCore;
using VideoGameBacklogTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Updated: Register the Database Context with a Retry Policy
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure() // Fixed: Handles the transient failure error
    ));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Games}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();