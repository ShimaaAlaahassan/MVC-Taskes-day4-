using Microsoft.AspNetCore.Mvc;
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

    }
}
