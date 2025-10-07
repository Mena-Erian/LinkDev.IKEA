using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Common
{
    public class QueryParameters
    {
        private int _pageSize = 10;
        private const int maxPageSize = 20;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > maxPageSize) _pageSize = maxPageSize;

                if (value < 1) _pageSize = 10;

                _pageSize = value;
            }
        }
    }
}
