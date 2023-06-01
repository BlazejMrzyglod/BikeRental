using AutoMapper;
using BikeRental.Models.Models;
using BikeRental.Services.Data;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ReservationsController : Controller
    {
        private readonly IRepositoryService<Reservation> _reservationRepository;
		private readonly IRepositoryService<Models.Models.Vehicle> _vehicleRepository;
		private readonly IMapper _mapper;

        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
			_reservationRepository = new RepositoryService<Models.Models.Reservation>(context);
			_vehicleRepository = new RepositoryService<Models.Models.Vehicle>(context);
			_mapper = mapper;
        }
        // GET: ReservationsController
        public ActionResult Index()
        {
            var reservations = _reservationRepository.GetAllRecords();
            List<ReservationViewModel> reservationsViewModels = new();
            foreach (var reservation in reservations)
            {
                reservationsViewModels.Add(_mapper.Map<ReservationViewModel>(reservation));
            }
            return View(reservationsViewModels);
        }

        // GET: ReservationsController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var reservation = _reservationRepository.GetSingle(id);
            return View(_mapper.Map<ReservationViewModel>(reservation));
        }

        // POST: ReservationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind("VehicleId,Status")] ReservationViewModel reservation)
        {
			try
            {
                if (reservation.Status==Models.ViewModels.Status.Realizacja)
                {
					var _reservation = _reservationRepository.GetSingle(id);
					_reservation.Status = Models.Models.Status.Wypożyczone;
                    _reservationRepository.Edit(_reservation);
                    _reservationRepository.Save();
				}
				if (reservation.Status == Models.ViewModels.Status.Wypożyczone)
				{
					var _reservation = _reservationRepository.GetSingle(id);
					_reservation.Status = Models.Models.Status.Zwrócone;
					_reservationRepository.Edit(_reservation);
					_reservationRepository.Save();
					var vehicle = _vehicleRepository.GetSingle((Guid)reservation.VehicleId);
					vehicle.Availability = true;
					_vehicleRepository.Edit(vehicle);
					_vehicleRepository.Save();
				}
				return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(reservation);
            }
        }
    }
}
