using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Areas.Users.Controllers
{
	public class ReservationsController : Controller
	{
		// GET: ReservationsController
		public ActionResult Index()
		{
			return View();
		}

		// POST: ReservationsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(IFormCollection collection)
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
