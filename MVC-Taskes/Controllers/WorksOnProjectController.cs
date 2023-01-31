using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Taskes.Models;

namespace MVC_Taskes.Controllers
{
    public class WorksOnProjectController : Controller
    {
        private CompanyDBContext db;
        public WorksOnProjectController()
        {
            db = new CompanyDBContext();
        }
        public IActionResult Index()
        {
            List<WorksOnProject> worksOnProjects = db.WorksOnProjects
             .Include(s => s.Employees).Include(w => w.Project).ToList();
            return View(worksOnProjects);

        }
        public IActionResult AddEmployeesToProjects(int id)
        {
            List<Project> projects = db.Projects.Where(p => p.DeptNum == id).ToList();
            List<Employee> employees = db.Employees.Where(p => p.deptId == id).ToList();

            ViewBag.emps = employees;

            return View(projects);
        }

        WorksOnProject worksOnProject1;
        public IActionResult AddEmployeesToProjectsToDB(List<int> Projects, List<int> Employees)
        {

            foreach (var Project in Projects)
            {
                foreach (var employee in Employees)
                {
                    WorksOnProject worksOnProject = new WorksOnProject()
                    {
                        EmpSSN = employee,
                        projNum = Project
                    };
                    worksOnProject1 = db.WorksOnProjects.Include(wop => wop.Project).SingleOrDefault(wop => wop.EmpSSN == worksOnProject.EmpSSN);
                    db.WorksOnProjects.Add(worksOnProject);
                    db.SaveChanges();
                }

            }

            ViewBag.emps = Employees;
            ViewBag.mgrSSN = (int)HttpContext.Session.GetInt32("SSN");

            return View(worksOnProject1);
        }
        [HttpGet]
        public IActionResult EditEmpHours()
        {
            List<Employee> employees = db.Employees.ToList();
            ViewBag.Emps = new SelectList(employees,"SSN","Fname");

            return View();
        }

        public IActionResult EditEmployeeHour_emp(int id)
        {
            List<Project>? projects = db.WorksOnProjects.Include(wop => wop.Project).Where(wop => wop.EmpSSN == id).Select(wop => wop.Project).ToList();
            ViewBag.projects = new SelectList(projects, "Number", "Name");
            if (projects.Count > 0)
            {
                WorksOnProject worksOnProject = new WorksOnProject()
                {
                    Hours = db.WorksOnProjects.SingleOrDefault(wop => (wop.EmpSSN == id) && (wop.projNum == projects[0].Number)).Hours
                };
                return PartialView("_ProjectsList", worksOnProject);
            }
            return PartialView("_ProjectsList");
        }
        public IActionResult EditEmployeeHour_emp_proj(int id, int projNum)
        {
            WorksOnProject? worksOnProject = db.WorksOnProjects
             .SingleOrDefault(wop => wop.EmpSSN == id && wop.projNum == projNum);
            
            return PartialView("_hour", worksOnProject);
        }
        [HttpPost]
        public IActionResult EditEmpHours( WorksOnProject works)
        {
           
                db.WorksOnProjects.Update(works);
                db.SaveChanges();
                 return View();


        }


    }
}
