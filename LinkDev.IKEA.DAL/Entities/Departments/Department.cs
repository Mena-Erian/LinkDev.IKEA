using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Departments
{
    public class Department : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }

        public int? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
