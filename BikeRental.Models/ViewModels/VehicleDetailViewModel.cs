using BikeRental.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BikeRental.Models.ViewModels
{
    public class VehicleDetailViewModel
    {
        [Required]
        [Display(Name="Identyfikator")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name="Producent")]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name="Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public int Price { get; set;}

        [Required]
        [Display(Name = "Lokacja")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Typ")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Zdjęcie")]
        public string Image { get; set; }

    }
}
