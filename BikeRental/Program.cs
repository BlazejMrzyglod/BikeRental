using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BikeRental.Models.MapperProfiles;
using BikeRental.Validation;
using BikeRental.Models.Models;
using BikeRental.Services.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("test"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews().AddViewOptions(options=>options.HtmlHelperOptions.ClientValidationEnabled = true);

builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

builder.Services.AddAutoMapper(typeof(VehiclesProfile), typeof(LocationsProfile), typeof(RolesProfile));

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

        var vehiclesRepository = scope.ServiceProvider.GetService<IRepositoryService<Vehicle>>();
		var vehicleTypesRepository = scope.ServiceProvider.GetService<IRepositoryService<VehicleType>>();
		var locationsRepository = scope.ServiceProvider.GetService<IRepositoryService<Location>>();
		var reservationsRepository = scope.ServiceProvider.GetService<IRepositoryService<Reservation>>();

		Guid ulicznyGuid = Guid.NewGuid();
		Guid gorskiGuid = Guid.NewGuid();
		Guid hybrydaGuid = Guid.NewGuid();
		Guid bmxGuid = Guid.NewGuid();
		Guid skladanyGuid = Guid.NewGuid();
		Guid turystycznyGuid = Guid.NewGuid();
		Guid elektrycznyGuid = Guid.NewGuid();
		vehicleTypesRepository.Add(new VehicleType { Id = ulicznyGuid, Name = "Uliczny" });
		vehicleTypesRepository.Add(new VehicleType { Id = gorskiGuid, Name = "Górski" });
		vehicleTypesRepository.Add(new VehicleType { Id = hybrydaGuid, Name = "Hybryda" });
		vehicleTypesRepository.Add(new VehicleType { Id = bmxGuid, Name = "BMX" });
		vehicleTypesRepository.Add(new VehicleType { Id = skladanyGuid, Name = "Sk³adany" });
		vehicleTypesRepository.Add(new VehicleType { Id = turystycznyGuid, Name = "Turystyczny" });
		vehicleTypesRepository.Add(new VehicleType { Id = elektrycznyGuid, Name = "Elektryczny" });
        vehicleTypesRepository.Save();

        Guid partyzantowGuid = Guid.NewGuid();
		Guid willowaGuid = Guid.NewGuid();
		Guid majaGuid = Guid.NewGuid();
		locationsRepository.Add(new Location { Id = partyzantowGuid, Name = "Partyzantów" });
		locationsRepository.Add(new Location { Id = willowaGuid, Name = "Willowa" });
		locationsRepository.Add(new Location { Id = majaGuid, Name = "3 Maja" });
        locationsRepository.Save();

        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Trek",
            Model = "FX 3",
            Price = 800,
            LocationId = majaGuid,
            Availability = true,
            Description = "A versatile hybrid bike for fitness and commuting.",
            Image = "fx3.jpg",
            TypeId = hybrydaGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Giant",
            Model = "Anthem Advanced Pro 29",
            Price = 5000,
            LocationId = partyzantowGuid,
            Availability = true,
            Description = "A high-performance full-suspension mountain bike.",
            Image = "anthem.jpg",
            TypeId = gorskiGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Specialized",
            Model = "Roubaix",
            Price = 2500,
            LocationId = willowaGuid,
            Availability = true,
            Description = "An endurance road bike with a smooth ride.",
            Image = "roubaix.jpg",
            TypeId = ulicznyGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Cannondale",
            Model = "Synapse",
            Price = 2000,
            LocationId = majaGuid,
            Availability = true,
            Description = "A comfortable and efficient road bike for long rides.",
            Image = "synapse.jpg",
            TypeId = ulicznyGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Scott",
            Model = "Scale 910",
            Price = 2500,
            LocationId = partyzantowGuid,
            Availability = true,
            Description = "A lightweight and fast cross-country mountain bike.",
            Image = "scale.jpg",
            TypeId = gorskiGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Canyon",
            Model = "Grail",
            Price = 3000,
            LocationId = willowaGuid,
            Availability = true,
            Description = "An adventure gravel bike with innovative features.",
            Image = "grail.jpg",
            TypeId = gorskiGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "BMC",
            Model = "Teammachine",
            Price = 4000,
            LocationId = majaGuid,
            Availability = true,
            Description = "A high-performance road racing bike.",
            Image = "teammachine.jpg",
            TypeId = ulicznyGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Santa Cruz",
            Model = "Nomad",
            Price = 6000,
            LocationId = partyzantowGuid,
            Availability = true,
            Description = "A full-suspension enduro mountain bike for aggressive riding.",
            Image = "nomad.jpg",
            TypeId = gorskiGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Fuji",
            Model = "Jari",
            Price = 1200,
            LocationId = willowaGuid,
            Availability = true,
            Description = "A versatile gravel bike for adventure and exploration.",
            Image = "jari.jpg",
            TypeId = gorskiGuid,
        });
        vehiclesRepository.Add(new Vehicle
        {
            Id = Guid.NewGuid(),
            Manufacturer = "Yeti",
            Model = "SB150",
            Price = 7000,
            LocationId = majaGuid,
            Availability = true,
            Description = "A high-end full-suspension trail bike for aggressive trail riding.",
            Image = "sb150.jpg",
            TypeId = gorskiGuid,
        });
        vehiclesRepository.Save();
	}
}

app.Run();
