using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Taskes.Models;

namespace MVC_Taskes.View_Model
{
    public class MangerNameVM
    {
        public SelectList empmanager;
        public int Number { get; set; }
        public string? Name { get; set; }
        public int? mngrSSN { get; set; }
        public string? Fname { get; set; }

        public virtual Employee? employeeManege { get; set; }
       // public SelectList manager { get; set; }

    }
}
