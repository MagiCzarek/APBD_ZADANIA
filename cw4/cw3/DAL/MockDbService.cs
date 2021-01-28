using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> studentsList;

        static MockDbService()
        {

        }
        public IEnumerable<Student> GetStudents()
        {
            return studentsList;
            throw new NotImplementedException();
        }
    }
}
