using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI_Template.Data;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Infrastructure.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {

    }
}
