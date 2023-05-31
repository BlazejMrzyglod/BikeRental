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
        private readonly IRepositoryService<Models.Models.Reservation> _repository;
        private readonly IMapper _mapper;

        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
            _repository = new RepositoryService<Models.Models.Reservation>(context);
            _mapper = mapper;
        }
        public ActionResult Index(Guid id)
        {
            return View();
        }
        // GET: ReservationsController/Create/5
        public async Task<IActionResult> Create(Guid id)
		{
			return View();
		}

        // POST: ReservationsController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, [Bind("StartDate,EndDate,User")] ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Id = Guid.NewGuid();
                reservation.ReservationDate = DateTime.Now;
                reservation.IsActive = true;
                reservation.VehicleId = id;
                _repository.Add(_mapper.Map<Models.Models.Reservation>(reservation));
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

    }
}
