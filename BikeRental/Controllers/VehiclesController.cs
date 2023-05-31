using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models.Models;
using BikeRental.Services.Repository;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Data;
using BikeRental.Models.ViewModels;
using AutoMapper;
using Microsoft.CodeAnalysis;

namespace BikeRental.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepositoryService<Vehicle> _repository;
        private readonly IMapper _mapper;

        public VehiclesController(ApplicationDbContext context, IMapper mapper)
        {
            _repository = new RepositoryService<Vehicle>(context);
            _mapper = mapper;
        }

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicles = _repository.GetAllRecords();
            List<VehicleItemViewModel> vehiclesViewModels = new();
            foreach (var vehicle in vehicles)
            {
                vehiclesViewModels.Add(_mapper.Map<VehicleItemViewModel>(vehicle));
            }
            return vehicles != null ?
                          View(vehiclesViewModels) :
                          Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(Guid id)
        {
            var vehicle = _repository.GetAllRecords().Where(x => x.Id == id).Include(x => x.Type).Include(x => x.Location).Single();
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
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
        public ActionResult Create([Bind("Id,Manufacturer,Model,Price,Location,Availability,Description,Image,Type")] VehicleDetailViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid();
                _repository.Add(_mapper.Map<Vehicle>(vehicle));
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (_repository.GetAllRecords() == null)
            {
                return NotFound();
            }
            var vehicle = _repository.GetAllRecords().Where(x => x.Id == id).Include(x => x.Type).Include(x => x.Location).Single();
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind("Status")] VehicleDetailViewModel vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(_mapper.Map<Vehicle>(vehicle));
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
        public ActionResult Delete(Guid? id)
        {
            if (id == null || _repository.GetAllRecords() == null)
            {
                return NotFound();
            }

            var vehicle = _repository.GetAllRecords().Where(x => x.Id == id).Include(x => x.Type).Include(x => x.Location).Single();

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VehicleDetailViewModel>(vehicle));
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (_repository.GetAllRecords() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
            }
            var vehicle = _repository.GetAllRecords().Include(x => x.Type).Include(x => x.Location)
                                     .FirstOrDefault(m => m.Id == id);

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
