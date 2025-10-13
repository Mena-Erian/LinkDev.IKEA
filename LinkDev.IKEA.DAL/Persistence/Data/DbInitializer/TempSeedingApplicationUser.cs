using LinkDev.IKEA.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.DbInitializer
{
    internal class TempSeedingApplicationUser : ApplicationUser
    {
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
