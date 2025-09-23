using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Common.Entities
{
    internal class BaseAuditableEntity<TKey> : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public required string LastModifiedBy { get; set; }
    }
}
