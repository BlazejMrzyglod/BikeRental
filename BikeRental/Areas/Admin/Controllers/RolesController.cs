using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        public RolesController(Services.ApplicationDbContext context) 
        {

        }
        // GET: RolesController
        public ActionResult Index()
        {
            return View();
        }

    }
}
