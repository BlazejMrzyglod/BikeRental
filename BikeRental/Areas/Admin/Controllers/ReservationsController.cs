using AutoMapper;
using BikeRental.Models.Models;
using BikeRental.Services.Data;
using BikeRental.Services.Repository.EntityFramework;
using BikeRental.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Models.ViewModels;

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

        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        

        // GET: ReservationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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

        // GET: ReservationsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
