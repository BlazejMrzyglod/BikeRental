using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BikeRental.Validation;
using BikeRental.Models.MapperProfiles;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BikeRental.Services.ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("test"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BikeRental.Services.ApplicationDbContext>();
builder.Services.AddControllersWithViews().AddViewOptions(options=>options.HtmlHelperOptions.ClientValidationEnabled = true);

builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

builder.Services.AddAutoMapper(typeof(VehiclesProfile), typeof(LocationsProfile));

builder.Services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReservationValidator>());


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
