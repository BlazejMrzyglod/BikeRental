using AutoMapper;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models.ViewModels;
using BikeRental.Services.Data;

namespace BikeRental.Areas.Users.Controllers
{
    [Area("Users")]
    public class ReservationsController : Controller
	{
        private readonly IRepositoryService<Models.Models.Reservation> _repository;
        private readonly IMapper _mapper;

        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
            _repository = new RepositoryService<Models.Models.Reservation>(context);
            _mapper = mapper;
        }
        // GET: ReservationsController/5
        public ActionResult Index(Guid id)
		{
			return View();
		}

        // POST: ReservationsController/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("StartDate,EndDate,User")] ReservationViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Id = Guid.NewGuid();
                reservation.ReservationDate = DateTime.Now;
                reservation.IsActive = true;
                _repository.Add(_mapper.Map<Models.Models.Reservation>(reservation));
                _repository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

    }
}
