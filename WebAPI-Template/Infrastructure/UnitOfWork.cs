using System;
using System.Threading.Tasks;
using WebAPI_Template.Data;
using WebAPI_Template.Domain;
using WebAPI_Template.Infrastructure.Repositories;

namespace WebAPI_Template.Infrastructure
{
   
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
             Posts = new PostRepository(_context);
        }
        public IPostRepository Posts { get; private set; }

        public int SaveChanges()
        {
            var iResult = _context.SaveChanges();
            return iResult;
        }

        public async Task<int> SaveChangesAsync()
        {
            var iResult = await _context.SaveChangesAsync();
            return iResult;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
