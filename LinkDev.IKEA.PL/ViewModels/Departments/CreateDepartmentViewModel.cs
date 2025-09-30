using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class CreateDepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required ya Hamada")]
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
    }
}
