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
            List<Department> departments = db.Departments.Include(s=>s.DepartmentLocations).ToList();
           
            return View(departments);
        }
        public IActionResult Details(int? id)
        {
            Department? department = db.Departments.Include(d=>d.DepartmentLocations).Include(p=>p.Projects).Include(s => s.employeeManege).SingleOrDefault(t => t.Number == id);
            if (department == null)
                return View("Error");
            else
                return View(department);
        }
        public IActionResult Add()
        {
            List<Employee> empmanager = db.Employees.ToList();
            ViewBag.empMan = new SelectList(empmanager, "SSN", "Fname");
            return View();
        }
        public IActionResult AddToDb(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges(); 
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int? id)
        {
            
            Department? dept = db.Departments.Include(d => d.DepartmentLocations).SingleOrDefault(s => s.Number == id);
            List<Employee> employees = db.Employees.ToList();
            ViewBag.manager = new SelectList(employees, "SSN", "Fname");
            return View(dept);


        }
        public IActionResult SaveEdit(Department department)
        {
            //Department olddept=db.Departments.Include(m=>m.employeeManege.Fname).FirstOrDefault(s=>s.Number==department.Number);
            //olddept.Name = department.Name;
            //olddept.DepartmentLocations = department.DepartmentLocations;
            //olddept.mngrSSN = department.mngrSSN;
            db.Departments.Update(department);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int? id)
        {
            Department? department = db.Departments.SingleOrDefault(d => d.Number == id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetDepartmentByMgrId(int id)
        {
            Department? department = db.Departments.Include(d => d.DepartmentLocations).Include(d => d.Projects).SingleOrDefault(d => d.mngrSSN == id);
            if (department == null)
                return View("Error");
            else
                return View("GetDepartmentByMgrId", department);

        }
    }
}
