namespace LinkDev.IKEA.PL.ViewModels.Identity.Users
{
    public class UserDetailsViewModel
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public bool EmailIsConfirm { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();

    }
}
