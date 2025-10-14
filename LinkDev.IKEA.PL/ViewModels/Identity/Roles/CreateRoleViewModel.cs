namespace LinkDev.IKEA.PL.ViewModels.Identity.Roles
{
    public class CreateRoleViewModel
    {
        public CreateRoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public readonly string Id;
        public required string Name { get; set; }
        //public string? Description { get; set; }
        //public bool IsAdmin { get; set; } 
    }
}
