using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Domain;
using WebAPI_Template.Infrastructure.Repositories;

namespace WebAPI_Template.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();       
    }
}
