using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeRental.Data;
using BikeRental.Models.Models;
using BikeRental.Services.Repository;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Models.ViewModels;

namespace BikeRental.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IRepositoryService<Location> _repository;

        public LocationsController(Services.ApplicationDbContext context)
        {
            _repository = new RepositoryService<Location>(context);
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var locations = _repository.GetAllRecords();
            List<LocationViewModel> locationViewModels = new List<LocationViewModel>();
            foreach (var location in locations)
            {
                locationViewModels.Add(new LocationViewModel(location));
            }
            return _repository.GetAllRecords() != null ? 
                          View(locationViewModels) :
                          Problem("Entity set 'ApplicationDbContext.Locations'  is null.");
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var location = _repository.GetAllRecords()
                .FirstOrDefault(m => m.Id == id);
            var locationModel = new LocationViewModel(location);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Address")] LocationViewModel location)
        {
/*            if (ModelState.IsValid)
            {*/
                location.Id = Guid.NewGuid();
                _repository.Add(new Location(location));
                _repository.Save();
                return RedirectToAction(nameof(Index));
            //}
            //return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var location = _repository.GetSingle(id);
            var locationModel = new LocationViewModel(location);
            if (locationModel == null)
            {
                return NotFound();
            }
            return View(locationModel);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Address")] LocationViewModel location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    _repository.Edit(new Location(location));
                    _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}
            return View(location);*/
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var location = _repository.GetAllRecords()
                .FirstOrDefault(m => m.Id == id);
            var locationModel = new LocationViewModel(location);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_repository.GetAllRecords() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Locations'  is null.");
            }
            var location = _repository.GetSingle(id);
            if (location != null)
            {
               _repository.Delete(location);
            }

            _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(Guid id)
        {
          return (_repository.GetAllRecords()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
