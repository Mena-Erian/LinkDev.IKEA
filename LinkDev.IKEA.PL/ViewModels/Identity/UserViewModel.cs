namespace LinkDev.IKEA.PL.ViewModels.Identity
{
    public class UserViewModel
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();


    }
}
