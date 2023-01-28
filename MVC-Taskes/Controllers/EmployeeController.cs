using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Taskes.Models;

namespace MVC_Taskes.Controllers
{
    public class EmployeeController : Controller
    {
        private CompanyDBContext db;
        public EmployeeController()
        {
            db = new CompanyDBContext();
        }
        public IActionResult Index()
        {
            List<Employee> employees = db.Employees.ToList();
            return View(employees);
        }
        public IActionResult GetById(int? id)
        {
            Employee? employee = db.Employees.Include(s => s.Supervisor).Include(e => e.DepartmentManege).Where(e => e.SSN == id).SingleOrDefault();

            if (employee == null)
                return View("Error");
            else
                return View("GetById", employee);
        }
        public IActionResult Add()
        {
            List<Employee> employees = db.Employees.ToList();
            return View(employees);
        }

        public IActionResult AddToDb(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            List<Employee> employees = db.Employees.ToList();
            Employee? employee = db.Employees.Include(s => s.Supervisor).Where(e => e.SSN == id).SingleOrDefault();
            ViewBag.emp = employees;
            if (employee == null)
                return View("Error");
            else
                return View(employee);
        }

        public IActionResult EditDB(Employee employee)
        {
            Employee oldEmp = db.Employees.Where(e => e.SSN == employee.SSN).SingleOrDefault();
            oldEmp.Fname = employee.Fname;
            oldEmp.Lname = employee.Lname;
            oldEmp.Minit = employee.Minit;
            oldEmp.Address = employee.Address;
            oldEmp.Salary = employee.Salary;
            oldEmp.Sex = employee.Sex;
            oldEmp.SupervisorSSN = employee.SupervisorSSN;
            oldEmp.BirthDate = employee.BirthDate;

            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {

            Employee Employee = db.Employees.Where(e => e.SSN == id).SingleOrDefault();
            db.Employees.Remove(Employee);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginDB(Employee employeeTologin)
        {

            Employee employee = db.Employees.SingleOrDefault(e => e.SSN == employeeTologin.SSN && e.Fname == employeeTologin.Fname);

            if (employee == null)
                return View("Error");
            else
            {
                HttpContext.Session.SetInt32("SSN", employee.SSN);
                return GetById(employee.SSN);
            }

        }

        public IActionResult AllEmpManger()
        {

            List<Employee>? employees = db.Departments.Include(e => e.employeeManege).Where(e => e.mngrSSN != null).Select(e => e.employeeManege).ToList();

            return View(employees);

        }
    }
}
