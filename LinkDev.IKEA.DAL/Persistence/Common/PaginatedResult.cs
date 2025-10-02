using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Common
{
    public class PaginatedResult<T>
    {
        public required IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; } // 15 // Total Count of all data in database not just the current returned data 
        public int PageIndex { get; set; } // 2
        public int PageSize { get; set; } // 20 // number of item per page
        public int TotalPageCount => PageSize > 0 ?
                    (int)Math.Ceiling((decimal)TotalCount / PageSize)
                    : 0;
        public bool HasNextPage => PageIndex < TotalPageCount;
        public bool HasPerviousPage => PageIndex > 1;
    }
}
