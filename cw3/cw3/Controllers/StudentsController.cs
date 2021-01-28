using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using Microsoft.AspNetCore.Mvc;


namespace cw3.Controllers{

    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase{
        private IDbService dbService;

        public StudentsController(IDbService dbService) {
            this.dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent(string orderBy) {
            if (orderBy == "indexNumber") {
                return Ok(dbService.GetStudents().OrderBy(idx => idx.indexNumber));
            }
            return Ok(dbService.GetStudents());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id) {
            if (id == 1) {
                return Ok("Boguszewski");
            } else if (id == 2) {
                return Ok("XD");
            }
            else{
                return NotFound("Nie znaleziono studenta");
            }
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student){
            student.indexNumber = $"s{new Random().Next(1,20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id) {
            return Ok($"Aktualizacja studenta o id={id}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) {
            return Ok($"Usuwanie studenta o id={id}");
        }
    }
}
