using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Taskes.Models
{
    public class WorksOnProject
    {
        public string? Hours { get; set; }

        [ForeignKey("Employees")]
        public int EmpSSN { get; set; }
        public virtual Employee? Employees { get; set; }

        [ForeignKey("Project")]
        public int projNum { get; set; }
        public virtual Project? Project { get; set; }
    }
}
