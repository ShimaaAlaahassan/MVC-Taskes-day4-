using Microsoft.AspNetCore.Mvc;

namespace MVC_Taskes.Controllers
{
    public class CustomValidationController : Controller
    {
        public IActionResult ValidLocation(string Location)
        {
            if (Location.Contains("cairo"))
            {
                return Json(true);
            }
            else if (Location.Contains("giza"))
            {
                return Json(true);
            }
            else if (Location.Contains("alex"))
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
