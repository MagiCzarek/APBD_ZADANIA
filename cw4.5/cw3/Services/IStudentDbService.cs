using cw3.Models;

using cw3.Request;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Services
{
    public interface IStudentDbService
    {
        bool CheckStudiesDb(EnrollStudentRequest enrollStudent);

        bool PromoteStudents(Promotions promotions);

        public Enrollment GetEnrollment();

        public Student GetStudent(string index);
    }
}
