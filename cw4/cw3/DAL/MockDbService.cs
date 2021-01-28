using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL{
    public class MockDbService : IDbService{
        private static IEnumerable<Student> studentsList;

        static MockDbService(){
            studentsList = new List<Student>()
            {
                new Student{ idNumber = 1, firstName = "Cezary  ",lastName = "Boguszewski", indexNumber = "s1"},
                new Student{ idNumber = 2, firstName = "Cezary  ",lastName = "Boguszewski", indexNumber = "s2"},
                new Student{ idNumber = 3, firstName = "Cezary  ",lastName = "Boguszewski", indexNumber = "s3"},
                
            };

        }
        public IEnumerable<Student> GetStudents(){
            return studentsList;
            throw new NotImplementedException();
        }
    }
}
