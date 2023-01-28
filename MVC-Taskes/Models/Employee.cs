using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Taskes.Models
{
    public class Employee
    {
        [Key]
        public int SSN { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Minit { get; set; }
        public string? Sex { get; set; }
        public string? Address { get; set; }

        [Column(TypeName = "money")]
        public int? Salary { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public virtual List<WorksOnProject>? WorksOnProjects { get; set; }

        public virtual List<Dependent> Dependents { get; set; }

        public int? SupervisorSSN { get; set; }
        public virtual Employee? Supervisor { get; set; }
        public List<Employee>? Employees { get; set; }
        public virtual Department? DepartmentManege { get; set; }
        [ForeignKey("DepartmentWork")]
        public int? deptId { get; set; }
        public virtual Department? DepartmentWork { get; set; }
    }
}
