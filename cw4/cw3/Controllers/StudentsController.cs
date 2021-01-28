using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;


namespace cw3.Controllers{

    [ApiController]
    [Route("api/students")]

      public class StudentsController : ControllerBase{

        private const string Conncetion = "Data Source=db-mssql;Initial Catalog=s189823;Integrated Security=True";

        [HttpGet]
        public IActionResult GetStudent() {
            List<Student> studentsList = new List<Student>();
            Student student;
            using (SqlConnection conn = new SqlConnection(Conncetion))
            using (SqlCommand command = new SqlCommand()) {
                command.Connection = conn;
                command.CommandText = "select * from student";

                conn.Open();
                SqlDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read()) {
                    student = new Student();
                    student.IndexNumber = sqlReader["IndexNumber"].ToString();
                    student.FirstName = sqlReader["FirstName"].ToString();
                    student.LastName = sqlReader["LastName"].ToString();
                    student.Birthdate = sqlReader["BirthDate"].ToString();
                    student.IdEnrollment = Int32.Parse(sqlReader["IdEnrollment"].ToString());
                    studentsList.Add(student);
                }
                sqlReader.Close();
            }
            return Ok(studentsList);
        }

        [HttpGet("{IndexNumber}")]
        public IActionResult GetStudent(string IndexNumber) {
            Student student;
            Enrollment enrollment = new Enrollment();
            Studies studies = new Studies();
            List<Object> query = new List<object>();
            using (SqlConnection conn = new SqlConnection(Conncetion))
            using (SqlCommand command = new SqlCommand()){
                command.Connection = conn;
                command.CommandText = "select Student.idEnrollment, semester, name, startdate from Enrollment inner join student on Enrollment.IdEnrollment = Student.IdEnrollment inner join Studies on Enrollment.idStudy = Studies.idStudy where Student.IndexNumber = @index";

                command.Parameters.AddWithValue("index", IndexNumber);

                conn.Open();
                var sqlReader = command.ExecuteReader();
                if(sqlReader.Read()){
                    enrollment.IdEnrollment = Int32.Parse(sqlReader["IdEnrollment"].ToString());
                    enrollment.Semester = Int32.Parse(sqlReader["Semester"].ToString());
                    studies.Name = sqlReader["Name"].ToString();
                    enrollment.Startdate = sqlReader["StartDate"].ToString();

                    query.Add(enrollment.IdEnrollment);
                    query.Add(enrollment.Semester);
                    query.Add(studies.Name);
                    query.Add(enrollment.Startdate);
                    return Ok(query);
                }
                sqlReader.Close();
                return NotFound();
            }
        }
    }
}
