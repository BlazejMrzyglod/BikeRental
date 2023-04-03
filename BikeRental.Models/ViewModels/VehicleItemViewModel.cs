﻿using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Xml.Linq;

namespace BikeRental.Models
{
    public class VehicleItemViewModel : IEntity<Guid>
    {
        [Display(Name = "Identyfikator")]
        public Guid Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public int Price { get; set; }
        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }
        [Display(Name = "Zdjęcie")]
        public string? Image { get; set; }
    }
}
