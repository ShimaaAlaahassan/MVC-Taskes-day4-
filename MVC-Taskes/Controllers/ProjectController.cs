using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Taskes.Models;
using MVC_Taskes.View_Model;

namespace MVC_Taskes.Controllers
{
    public class ProjectController : Controller
    {
        private CompanyDBContext db;
        public ProjectController()
        {
            db = new CompanyDBContext();
        }
        //display all project using tag helper
        public IActionResult Index()
        {
            List<Project> projects = db.Projects.ToList();
            return View(projects);
        }

        public IActionResult Detailes(int id)
        {
            Project project = db.Projects.SingleOrDefault(e => e.Number == id);
            return View(project);
        }
        [HttpGet]
       
        public IActionResult Add()
        {
            List<Department> departments = db.Departments.ToList();
            ViewBag.depts = new SelectList(departments, "Number", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProjectVM project)
        {
            if (ModelState.IsValid)
            {
                Project newProject = new Project()
                {
                    Name = project.Name,
                    Location = project.Location,
                    //DeptNum=project.DeptNum,
                };
                db.Projects.Add(newProject);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
           
        }
        //public IActionResult Add(Project project)
        //{

        //    db.Projects.Add(project);
        //    db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            
            List<Department> departments = db.Departments.ToList();
            ViewBag.depts = new SelectList(departments, "Number", "Name");
            if (id == null)
            {
                return View("Error");
            }
            Project project = db.Projects.SingleOrDefault(s => s.Number == id);
            return View(project);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(ProjectVM project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Project oldeproject = db.Projects.SingleOrDefault(s => s.Number == project.Number);
        //        oldeproject.Name = project.Name;
        //        oldeproject.Location = project.Location;

        //        db.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();

        //}
        [HttpPost]
        
        public IActionResult Edit(Project project)
        {
            Project oldeproject = db.Projects.SingleOrDefault(s => s.Number == project.Number);
            oldeproject.Name = project.Name;
            oldeproject.Location = project.Location;
            oldeproject.DeptNum = project.DeptNum;
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            Project project = db.Projects.SingleOrDefault(s => s.Number == id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
