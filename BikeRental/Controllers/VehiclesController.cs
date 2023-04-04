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

namespace BikeRental.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepositoryService<Vehicle> _repository;

        public VehiclesController(ApplicationDbContext context)
        {
            _repository = new RepositoryService<Vehicle>(context);
        }
        /*_repository.Add(new Vehicle() { Id = new Guid(), Manufacturer = "dasdasd", Price = 12313, Availability = true});
            _repository.Save();*/
        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicles = _repository.GetAllRecords().AsEnumerable();
            return vehicles != null ? 
                          View(vehicles) :
                          Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle = await _repository.GetAllRecords()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
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
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Price,LocationId,Availability,Description,Image,TypeId,ReservationId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid();
                _repository.Add(vehicle);
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle =  _repository.GetSingle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Manufacturer,Model,Price,LocationId,Availability,Description,Image,TypeId,ReservationId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(vehicle);
                    _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle = await _repository.GetAllRecords()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_repository.GetAllRecords() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
            }
            var vehicle = _repository.GetSingle(id);
            if (vehicle != null)
            {
                _repository.Delete(vehicle);
            }

            _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(Guid id)
        {
            return (_repository.GetAllRecords()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
