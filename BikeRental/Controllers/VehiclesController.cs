using BikeRental.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Xml.Linq;

namespace BikeRental.Controllers
{
    public class VehiclesController : Controller
    {
        static List<VehicleDetailViewModel> vehiclesDetails = new List<VehicleDetailViewModel>()
        {
            new VehicleDetailViewModel(){Id = 1, Manufacturer = "safdasfasf", Availability = true, Description = "fadsfdsafasdf", Location = "cdsjfbasdjf", Model = "dasfafasf", Price = 1234 },
            new VehicleDetailViewModel(){Id = 2, Manufacturer = "dwa", Availability = true, Description = "fadsfdsafasdf", Location = "cdsjfbasdjf", Model = "dasfafasf", Price = 1234 },
            new VehicleDetailViewModel(){Id = 3, Manufacturer = "trzy", Availability = true, Description = "fadsfdsafasdf", Location = "cdsjfbasdjf", Model = "dasfafasf", Price = 1234 },
        };
        static List<VehicleItemViewModel> vehicles = new List<VehicleItemViewModel>()
        {
            new VehicleItemViewModel() { Id = 1, Availability = true, Name = "jeden", Price = 1234 },
            new VehicleItemViewModel() { Id = 2, Availability = true, Name = "dwa", Price = 1234 },
            new VehicleItemViewModel() { Id = 3, Availability = true, Name = "trzy", Price = 1234 },
        };

        // GET: VehiclesController
        public ActionResult Index()
        {
            return View(vehicles);
        }

        // GET: VehiclesController/Details/5
        public ActionResult Details(int id)
        {
            var vehicle = vehiclesDetails.FirstOrDefault(x => x.Id == id);
            return View(vehicle);

        }
    }
}








