namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeListViewModel
    {
        public required IEnumerable<EmployeeViewModel> Employees { get; set; }

        // Pagination Properties

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);


        // Filtering Properties
        public string? SearchTerm { get; set; }

        // Sorting Properties
        public string? SortBy { get; set; }
        public bool SortAscending { get; set; } = true;
    }
}
