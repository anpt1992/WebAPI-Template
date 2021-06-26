using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Template.Contracts.V2.Requests.Queries
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            X = 0;
            PageNumber = 1;
            PageSize = 100;
        }

        public PaginationQuery(int pageNumber, int pageSize,int x)
        {
            X = x;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int X { get; set; }
    }
}
