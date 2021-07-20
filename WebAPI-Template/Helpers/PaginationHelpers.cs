using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Contracts;
using WebAPI_Template.Contracts.V1.Requests.Queries;
using WebAPI_Template.Contracts.V1.Responses;
using WebAPI_Template.Domain;
using WebAPI_Template.Services;

namespace WebAPI_Template.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService,PaginationFilter pagination, List<T> response)
        {
            var nextPage = pagination.PageNumber >= 1 ? uriService.GetAllPostUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString() : null;
            var previousPage = pagination.PageNumber - 1 >= 1 ? uriService.GetAllPostUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString() : null;

            return  new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviosPage = previousPage
            };
        }

       
    }
}
