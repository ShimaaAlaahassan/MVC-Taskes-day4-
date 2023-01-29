using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Taskes.Models;
using MVC_Taskes.View_Model;

namespace MVC_Taskes.Controllers
{
    public class DepartmentController : Controller
    {
        private CompanyDBContext db;
        public DepartmentController()
        {
            db = new CompanyDBContext();
        }
        //to display all department
        public IActionResult Index()
        {
            List<Department> departments = db.Departments.Include(s=>s.DepartmentLocations).Include(d=>d.employeeManege).ToList();
           
            return View(departments);
        }
        public IActionResult Details(int id)
        {
            Department department = db.Departments.Include(s => s.employeeManege).SingleOrDefault(t => t.Number == id);
            MangerNameVM vM = new MangerNameVM();
            vM.Name = department.Name;
            vM.mngrSSN = department.mngrSSN;
            return View(vM);
        }
        public IActionResult Add()
        {
            List<Employee> empmanager = db.Employees.Include(s => s.DepartmentManege).ToList();
            ViewBag.empMan = new SelectList(empmanager, "SSN", "Fname");
            return View();
        }
        public IActionResult AddToDb(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges(); 
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {

            List<Employee> employees = db.Employees.ToList();
            ViewBag.manager = employees;
            Department dept = db.Departments.SingleOrDefault(s => s.Number == id);
            return View(dept);

        }
        public IActionResult SaveEdit(Department department)
        {
            Department olddept=db.Departments.Include(m=>m.employeeManege.Fname).FirstOrDefault(s=>s.Number==department.Number);
            olddept.Name = department.Name;
            olddept.DepartmentLocations = department.DepartmentLocations;
            olddept.mngrSSN = department.mngrSSN;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            Department department = db.Departments.SingleOrDefault(d => d.Number == id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetDepartmentByMgrId(int id)
        {
            Department department = db.Departments.Include(d => d.DepartmentLocations).Include(d => d.Projects).SingleOrDefault(d => d.mngrSSN == id);
            if (department == null)
                return View("Error");
            else
                return View("GetDepartmentByMgrId", department);

        }
    }
}
