using BikeRental.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BikeRental.Models.ViewModels
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
        [Display(Name = "Typ")]
        public string Type { get; set; }
        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Image { get; set; }

        public VehicleDetailViewModel(Vehicle vehicle)
        {
            Id = vehicle.Id;
            Manufacturer = vehicle.Manufacturer;
            Model = vehicle.Model;
            Price = vehicle.Price;
            Location = vehicle.Location.Address;
            Availability = vehicle.Availability;
            Description = vehicle.Description;
            Image = vehicle.Image;
            Type = vehicle.Type.Type;
        }
        public VehicleDetailViewModel()
        {

        }
    }
}
