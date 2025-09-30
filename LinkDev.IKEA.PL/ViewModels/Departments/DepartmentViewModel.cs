using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
    }
}
