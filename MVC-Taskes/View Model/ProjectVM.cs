using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Taskes.View_Model
{
    public class ProjectVM
    {
        public int Number { get; set; }
        [Display(Name ="Project Name")]
        [MinLength(5,ErrorMessage ="Name must be 5 charachter or  more than 5")]
        [Required(ErrorMessage ="name is required")]
       
        public string? Name { get; set; }
        [Required(ErrorMessage ="location is required")]
        [Display(Name ="project location")]
        [Remote("ValidLocation", "CustomValidation",AdditionalFields ="Location")]
        public string? Location { get; set; }
        [Compare("Location")] 
        public string? ConfirmLocation { get; set; }

        //[Display(Name = "department")]
        //public int? DeptNum { get; set; }
    }
}
