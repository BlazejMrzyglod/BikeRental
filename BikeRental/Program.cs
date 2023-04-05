using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models;
using BikeRental.Data;
using Microsoft.Extensions.DependencyInjection;

/*List<VehicleDetailViewModel> vehiclesDetails = new List<VehicleDetailViewModel>()
        {
            new VehicleDetailViewModel(){Id = 1, Manufacturer = "safdasfasf", Availability = true, Description = "fadsfdsafasdf", Location = "cdsjfbasdjf", Model = "dasfafasf", Price = 1234 },
            new VehicleDetailViewModel(){Id = 2, Manufacturer = "dwa", Availability = true, Description = "fadsfdsafasdf", Location = "cdsjfbasdjf", Model = "dasfafasf", Price = 1234 },
            new VehicleDetailViewModel(){Id = 3, Manufacturer = "trzy", Availability = true, Description = "fadsfdsafasdf", Location = "cdsjfbasdjf", Model = "dasfafasf", Price = 1234 },
        };
List<VehicleItemViewModel> vehicles = new List<VehicleItemViewModel>()
        {
            new VehicleItemViewModel() { Id = 1, Availability = true, Name = "jeden", Price = 1234 },
            new VehicleItemViewModel() { Id = 2, Availability = true, Name = "dwa", Price = 1234 },
            new VehicleItemViewModel() { Id = 3, Availability = true, Name = "trzy", Price = 1234 },
        };*/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BikeRental.Services.ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("test"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BikeRental.Services.ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
app.MapRazorPages();

app.Run();
