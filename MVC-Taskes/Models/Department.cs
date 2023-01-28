using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Taskes.Models
{
    public class Department
    {
        [Key]
        public int Number { get; set; }
        public string? Name { get; set; }
        public virtual List<DepartmentLocation>? DepartmentLocations { get; set; }
        public virtual List<Project>? Projects { get; set; }
        [ForeignKey("employeeManege")]
        public int? mngrSSN { get; set; }
        public virtual Employee? employeeManege { get; set; }
        public virtual List<Employee>? EmployeesWork { get; set; }
    }
}
