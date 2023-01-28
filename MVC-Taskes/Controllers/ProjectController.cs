using Microsoft.AspNetCore.Mvc;

namespace MVC_Taskes.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
