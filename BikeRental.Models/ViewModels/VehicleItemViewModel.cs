using BikeRental.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Xml.Linq;

namespace BikeRental.Models.ViewModels
{
    public class VehicleItemViewModel
    {
        [Required]
        [Display(Name = "Identyfikator")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }

        [Required]
        [Display(Name = "Zdjęcie")]
        public string? Image { get; set; }

    }
}
