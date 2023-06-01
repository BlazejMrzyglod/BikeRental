using AutoMapper;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models.ViewModels;
using BikeRental.Services.Data;
using Microsoft.AspNetCore.Authorization;

namespace BikeRental.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class ReservationsController : Controller
	{
        private readonly IRepositoryService<Models.Models.Reservation> _reservationRepository;
        private readonly IRepositoryService<Models.Models.Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;

        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
            _reservationRepository = new RepositoryService<Models.Models.Reservation>(context);
            _vehicleRepository = new RepositoryService<Models.Models.Vehicle>(context);
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: ReservationsController/Create/5
        public ActionResult Create()
		{
			return View();
		}

        // POST: ReservationsController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create(Guid id, [Bind("StartDate,EndDate,User")] ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Id = Guid.NewGuid();
                reservation.ReservationDate = DateTime.Now;
                reservation.Status = Status.Realizacja;
                reservation.VehicleId = id;
                _reservationRepository.Add(_mapper.Map<Models.Models.Reservation>(reservation));
                _reservationRepository.Save();
                var vehicle = _vehicleRepository.GetSingle(id);
                vehicle.Availability = false;
                _vehicleRepository.Edit(vehicle);
                _vehicleRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

    }
}
