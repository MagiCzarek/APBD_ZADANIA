using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.Models;
using cw3.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace cw3.Controllers
{
    [Route("api/students")]
    [ApiController]

    public class StudentController : ControllerBase
    {

        private readonly StudentsDbContext studentsDbContext;
        private string Date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;

        public StudentController(StudentsDbContext studentsDbContext)
        {
            this.studentsDbContext = studentsDbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(studentsDbContext.Student.ToList());
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteStudent(string index)
        {
            if (studentsDbContext.Student.Where(s => s.IndexNumber == index).Any())
            {
                var student = studentsDbContext.Student.SingleOrDefault(s => s.IndexNumber == index);
                studentsDbContext.Student.Remove(student);
                studentsDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ModifyStudent(ModifyStudent modifyStudent)
        {
            if (studentsDbContext.Student.Where(s => s.IndexNumber == modifyStudent.Index).Any())
            {
                var student = studentsDbContext.Student.SingleOrDefault(s => s.IndexNumber == modifyStudent.Index);
                if (student.IndexNumber != modifyStudent.IndexNumber && modifyStudent.IndexNumber != null)
                {
                    student.IndexNumber = modifyStudent.IndexNumber;
                    studentsDbContext.SaveChanges();
                }
                if (student.FirstName != modifyStudent.FirstName && modifyStudent.FirstName != null)
                {
                    student.FirstName = modifyStudent.FirstName;
                    studentsDbContext.SaveChanges();
                }
                if (student.LastName != modifyStudent.LastName && modifyStudent.LastName != null)
                {
                    student.LastName = modifyStudent.LastName;
                    studentsDbContext.SaveChanges();
                }
                if (student.Birthdate != modifyStudent.Birthdate && modifyStudent.Birthdate != null)
                {
                    student.Birthdate = modifyStudent.Birthdate;
                    studentsDbContext.SaveChanges();
                }
                if (student.IdEnrollment != modifyStudent.IdEnrollment && modifyStudent.IdEnrollment != null)
                {
                    student.IdEnrollment = modifyStudent.IdEnrollment;
                    studentsDbContext.SaveChanges();
                }
                return Ok();
            }
            return NotFound();
        }

        [Route("/api/students/enrollments")]
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            if (!(studentsDbContext.Student.Where(s => s.IndexNumber == request.IndexNumber).Any()))
            {
                if (studentsDbContext.Studies.Where(s => s.Name == request.Studies).Any())
                {
                    var studies = studentsDbContext.Studies.SingleOrDefault(s => s.Name == request.Studies);
                    var maxDate = studentsDbContext.Enrollment.Max(d => d.StartDate);

                    if (!(studentsDbContext.Enrollment.Where(e => (e.Semester == 1) && (e.IdStudy == studies.IdStudy) && (e.StartDate == maxDate)).Any()))
                    {
                        int id = studentsDbContext.Enrollment.Max(i => i.IdEnrollment);
                        Enrollment enrollment = new Enrollment
                        {
                            IdEnrollment = id,
                            Semester = 1,
                            IdStudy = studies.IdStudy,
                            StartDate = Convert.ToDateTime(Date)
                        };
                        studentsDbContext.Enrollment.Add(enrollment);
                        studentsDbContext.SaveChanges();
                    }

                    var enroll = studentsDbContext.Enrollment.SingleOrDefault(e => (e.Semester == 1) && (e.IdStudy == studies.IdStudy) && (e.StartDate == maxDate));
                    Student student = new Student
                    {
                        IndexNumber = request.IndexNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Birthdate = Convert.ToDateTime(request.Birthdate),
                        IdEnrollment = enroll.IdEnrollment
                    };
                    studentsDbContext.Student.Add(student);
                    studentsDbContext.SaveChanges();
                    return Ok();
                }
            }
            return NotFound();
        }

        [Route("/api/students/promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(Promotions promotions)
        {

            var res = from enroll in studentsDbContext.Enrollment
                      join studies in studentsDbContext.Studies on enroll.IdStudy equals studies.IdStudy
                      where enroll.Semester == promotions.Semester && studies.Name == promotions.Studies
                      select enroll;

            if (res != null)
            {
                object[] data = new object[2] { promotions.Studies, promotions.Semester };
                studentsDbContext.Database.ExecuteSqlRaw("EXEC procedura @StudiesName,@Semester", data);
                studentsDbContext.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }
}
