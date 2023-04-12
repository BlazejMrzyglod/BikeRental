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
using AutoMapper;
using Microsoft.CodeAnalysis;

namespace BikeRental.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IRepositoryService<Models.Models.Location> _repository;
        private readonly IMapper _mapper;

        public LocationsController(Services.ApplicationDbContext context, IMapper mapper)
        {
            _repository = new RepositoryService<Models.Models.Location>(context);
            _repository.Add(new Models.Models.Location() { Name = "dasd", Id = Guid.NewGuid() });
            _mapper = mapper;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var locations = _repository.GetAllRecords();
            List<LocationViewModel> locationViewModels = new List<LocationViewModel>();
            foreach (var location in locations)
            {
                locationViewModels.Add(_mapper.Map<LocationViewModel>(location));
            }
            return locations != null ?
                          View(locationViewModels) :
                          Problem("Entity set 'ApplicationDbContext.Location'  is null.");
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var location = _repository.GetAllRecords()
                .FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<LocationViewModel>(location));
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
        public async Task<IActionResult> Create([Bind("Name")] LocationViewModel location)
        {
            if (ModelState.IsValid)
            {
                location.Id = Guid.NewGuid();
                _repository.Add(_mapper.Map<Models.Models.Location>(location));
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var location = _repository.GetSingle(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<LocationViewModel>(location));
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] LocationViewModel location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(_mapper.Map<Models.Models.Location>(location));
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
            }
            return View(location);
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
            if (location == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<LocationViewModel>(location));
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
