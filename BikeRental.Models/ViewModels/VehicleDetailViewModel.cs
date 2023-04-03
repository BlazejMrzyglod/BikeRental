﻿using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BikeRental.Models
{
    public class VehicleDetailViewModel : IEntity<Guid>
    {
        [Display(Name="Identyfikator")]
        public Guid Id { get; set; }
        [Display(Name="Producent")]
        public string Manufacturer { get; set; }
        [Display(Name="Model")]
        public string Model { get; set; }
        [Display(Name = "Cena")]
        public int Price { get; set;}
        [Display(Name = "Lokacja")]
        public string Location { get; set; }
        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Image { get; set; }
    }
}
