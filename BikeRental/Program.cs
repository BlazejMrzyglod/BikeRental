using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BikeRental.Models.MapperProfiles;
using BikeRental.Validation;

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

/*var configuration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Location, LocationViewModel>();
    cfg.CreateMap<LocationViewModel, Location>().ForMember(dest => dest.Vehicles, opt => opt.Ignore());
    cfg.CreateMap<Vehicle, VehicleDetailViewModel>();
    cfg.CreateMap<VehicleDetailViewModel, Vehicle>().ForMember(dest => dest.Location, opt => opt.Ignore()).ForMember(dest => dest.LocationId, opt => opt.Ignore()).ForMember(dest => dest.TypeId, opt => opt.Ignore()).ForMember(dest => dest.Type, opt => opt.Ignore()).ForMember(dest => dest.ReservationId, opt => opt.Ignore()).ForMember(dest => dest.Reservation, opt => opt.Ignore());
    cfg.CreateMap<Vehicle, VehicleItemViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Manufacturer + ' ' + x.Model));
});
//only fo development
configuration.AssertConfigurationIsValid();*/

//var mapper = configuration.CreateMapper();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BikeRental.Services.ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("test"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var createResult = userManager.CreateAsync(
                 new IdentityUser()
                 {
                     UserName = "admin@ath.eu",
                     Email = "admin@ath.eu",
                     LockoutEnabled = false,
                     AccessFailedCount = 0,
                 }, "Az123456$");

    if (roleManager != null)
    {
        if (!roleManager.RoleExistsAsync("Administrators").Result)
            roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Administrator"
            });
        if (!roleManager.RoleExistsAsync("Operator").Result)
            roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Operator"
            });
        if (!roleManager.RoleExistsAsync("U¿ytkownik").Result)
            roleManager.CreateAsync(new IdentityRole()
            {
                Name = "U¿ytkownik"
            });

        var adminUser = userManager.FindByNameAsync("admin@ath.eu").Result;

        var code = userManager.GenerateEmailConfirmationTokenAsync(adminUser).Result;
        // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var result = userManager.ConfirmEmailAsync(adminUser, code).Result;

        userManager.AddToRoleAsync(adminUser, "Administrator");
    }
}

app.Run();
