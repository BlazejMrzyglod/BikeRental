using AutoMapper;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models.ViewModels;

namespace BikeRental.Areas.Users.Controllers
{
	public class ReservationsController : Controller
	{
        private readonly IRepositoryService<Models.Models.Reservation> _repository;
        private readonly IMapper _mapper;

        public ReservationsController(Services.ApplicationDbContext context, IMapper mapper)
        {
            _repository = new RepositoryService<Models.Models.Reservation>(context);
            _mapper = mapper;
        }
        // GET: ReservationsController
        public ActionResult Index()
		{
			return View();
		}

        // POST: ReservationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("ReservationDate,StartDate,EndDate,IsActive,Vehicles")] ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Id = Guid.NewGuid();
                _repository.Add(_mapper.Map<Models.Models.Reservation>(reservation));
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

    }
}
