using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Services
{
    public interface ITestService
    {
        public List<Test> GetTests();
        public Test GetTestById(Guid testId);
        bool UpdateTest(Test testToUpdate);
        bool DeleteTest(Guid testId);
    }
    public class TestService : ITestService
    {
        private readonly List<Test> _tests;

        public TestService()
        {
            _tests = new List<Test>();
            for (int i = 0; i < 5; i++)
            {
                _tests.Add(new Test { Id = Guid.NewGuid(), Name = $"Name{i}" });
            }
        }

        public Test GetTestById(Guid testId)
        {
            return _tests.SingleOrDefault(x => x.Id == testId);
        }

        public List<Test> GetTests()
        {
            return _tests;
        }
        public bool UpdateTest(Test testToUpdate)
        {
            var exists = GetTestById(testToUpdate.Id) != null;
            if (!exists)
                return false;
            var index = _tests.FindIndex(x => x.Id == testToUpdate.Id);
            _tests[index] = testToUpdate;
            return true;
        }
        public bool DeleteTest(Guid testId)
        {
            var test = GetTestById(testId);
            if (test == null)
                return false;

            _tests.Remove(test);
            return true;
        }

    }
}
