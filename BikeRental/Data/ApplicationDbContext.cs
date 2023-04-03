﻿using BikeRental.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models;

namespace BikeRental.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<BikeRental.Models.VehicleItemViewModel>? VehicleItemViewModel { get; set; }
        public DbSet<BikeRental.Models.VehicleDetailViewModel>? VehicleDetailViewModel { get; set; }

    }
}