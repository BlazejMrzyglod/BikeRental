using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models.Models;
using BikeRental.Services.Repository;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Models;
using BikeRental.Services;
using BikeRental.Data;
using BikeRental.Models.ViewModels;

namespace BikeRental.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepositoryService<Vehicle> _vehicleRepository;

        public VehiclesController(Services.ApplicationDbContext context)
        {
            _vehicleRepository = new RepositoryService<Vehicle>(context);
            _vehicleRepository.Add(new Vehicle() { Id = Guid.NewGuid(), Manufacturer = "dasdasd", Price = 12313, Availability = true, Description = "dasdas", Image = "dasda", Location = new Location() { Address = "dasd", Id = Guid.NewGuid() }, Model = "dasdas", Type = new VehicleType() { Id = Guid.NewGuid(), Type = "fasfasf" }});
            _vehicleRepository.Save();
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicles = _vehicleRepository.GetAllRecords();
            List<VehicleItemViewModel> vehiclesViewModels = new List<VehicleItemViewModel>();
            foreach (var vehicle in vehicles)
            {
                vehiclesViewModels.Add(new VehicleItemViewModel(vehicle));
            }
            return vehiclesViewModels != null ? 
                          View(vehiclesViewModels) :
                          Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _vehicleRepository.GetAllRecords() == null)
            {
                return NotFound();
            }
            var vehicle = _vehicleRepository.GetAllRecords().Include(x => x.Type).Include(x => x.Location)
               .FirstOrDefault(m => m.Id == id);
            VehicleDetailViewModel vehicleViewModel = new VehicleDetailViewModel(vehicle);
            if (vehicleViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleViewModel);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Price,Location,Availability,Description,Image,Type,ReservationId")] VehicleDetailViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid();
                var vehicleModel = new Vehicle(vehicle);
                _vehicleRepository.Add(vehicleModel);
                _vehicleRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _vehicleRepository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle = _vehicleRepository.GetAllRecords().Include(x => x.Type).Include(x => x.Location)
               .FirstOrDefault(m => m.Id == id);
            VehicleDetailViewModel vehicleViewModel = new VehicleDetailViewModel(vehicle);
            if (vehicleViewModel == null)
            {
                return NotFound();
            }
            return View(vehicleViewModel);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Manufacturer,Model,Price,Location,Availability,Description,Image,Type,ReservationId")] VehicleDetailViewModel vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }
            var vehicleModel = new Vehicle(vehicle);
            if (ModelState.IsValid)
            {
                try
                {       
                    _vehicleRepository.Edit(vehicleModel);
                    _vehicleRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicleModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _vehicleRepository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle = _vehicleRepository.GetAllRecords().Include(x => x.Type).Include(x => x.Location)
                .FirstOrDefault(m => m.Id == id);
            VehicleDetailViewModel vehicleViewModel = new VehicleDetailViewModel(vehicle);
            if (vehicleViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleViewModel);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_vehicleRepository.GetAllRecords() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
            }
            var vehicle = _vehicleRepository.GetAllRecords().Include(x => x.Type).Include(x => x.Location)
               .FirstOrDefault(m => m.Id == id);

            if (vehicle != null)
            {
                _vehicleRepository.Delete(vehicle);
            }

            _vehicleRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(Guid id)
        {
            return (_vehicleRepository.GetAllRecords()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
