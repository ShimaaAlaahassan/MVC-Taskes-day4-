using Microsoft.AspNetCore.Mvc;
using MVC_Taskes.Models;

namespace MVC_Taskes.Controllers
{
    public class DependentController : Controller
    {
        EmployeeController employeeController = new EmployeeController();
        private CompanyDBContext db;

        public DependentController()
        {
            db = new CompanyDBContext();
        }
        public IActionResult Index()
        {
            List<Dependent> dependents = db.Dependents.ToList();
            return View(dependents);
        }
        public IActionResult GetAllDependent()
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            List<Dependent> dependents = db.Dependents.Where(d => d.EmpSSN == SSNfromSession).ToList();

            return View(dependents);
        }
        Int32 SSNfromSession;

        public IActionResult Add()
        {

            return View();
        }
        public IActionResult AddToDb(Dependent dependent)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            dependent.EmpSSN = SSNfromSession;
            db.Dependents.Add(dependent);
            db.SaveChanges();
            TempData["AddMsg"] = "You Add New Dependent";

            return RedirectToAction(nameof(GetAllDependent));

        }

        public IActionResult Edit(string id)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            Dependent dependent = db.Dependents.SingleOrDefault(d => d.EmpSSN == SSNfromSession && d.Name == id);
            if (dependent == null)
                return View("Error");
            else
                return View(dependent);
        }
        public IActionResult EditToDb(Dependent dependentToEdit)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            Dependent olDdependent = db.Dependents.SingleOrDefault(d => d.EmpSSN == SSNfromSession && d.Name == dependentToEdit.Name);
            olDdependent.Sex = dependentToEdit.Sex;
            olDdependent.Relationship = dependentToEdit.Relationship;
            olDdependent.BirthDate = dependentToEdit.BirthDate;
            db.SaveChanges();
            return RedirectToAction(nameof(GetAllDependent));
        }

        public IActionResult Delete(string id)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            Dependent? dependentToDelete = db.Dependents.SingleOrDefault(d => d.EmpSSN == SSNfromSession && d.Name == id);
            db.Dependents.Remove(dependentToDelete);
            db.SaveChanges();
            return RedirectToAction(nameof(GetAllDependent));

        }
    }
}
