using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Data;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Services
{
    public interface ITestService
    {
        Task<List<Test>> GetTestsAsync();     
        Task<bool> CreateTestAsync(Test test);
        Task<Test> GetTestByIdAsync(Guid testId);
        Task<bool> UpdateTestAsync(Test testToUpdate);
        Task<bool> DeleteTestAsync(Guid testId);
    }
    public class TestService : ITestService
    {
        private readonly DataContext _dataContext;

        public TestService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Test>> GetTestsAsync()
        {
            return await _dataContext.Tests.ToListAsync();
        }

        public async Task<Test> GetTestByIdAsync(Guid testId)
        {
            return await _dataContext.Tests.SingleOrDefaultAsync(x => x.Id == testId);
        }
        public async Task<bool> CreateTestAsync(Test test)
        {
            await _dataContext.Tests.AddAsync(test);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }
        public async Task<bool> UpdateTestAsync(Test testToUpdate)
        {
            _dataContext.Tests.Update(testToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> DeleteTestAsync(Guid testId)
        {
            var test = await GetTestByIdAsync(testId);
            _dataContext.Tests.Remove(test);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

    }
}
