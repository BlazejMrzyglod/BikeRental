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
        private readonly IRepositoryService<Reservation> _repository;
        private readonly IMapper _mapper;

        public ReservationsController(ApplicationDbContext context, IMapper mapper)
        {
            _repository = new RepositoryService<Reservation>(context);
            _mapper = mapper;
        }
        // GET: ReservationsController
        public ActionResult Index()
        {
            var reservations = _repository.GetAllRecords();
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
           var reservation = _repository.GetAllRecords().Where(x => x.Id == id);
           return View(_mapper.Map<ReservationViewModel>(reservation));
        }

        // POST: ReservationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
