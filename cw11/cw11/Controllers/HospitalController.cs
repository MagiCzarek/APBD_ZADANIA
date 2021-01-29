using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw11.Models;
using cw11.Request;
using Microsoft.AspNetCore.Mvc;

namespace cw11.Controllers
{
    public class HospitalController : ControllerBase
    {
        private readonly HospitalDbContext hospitalDbContext;

        public HospitalController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            if (hospitalDbContext.Doctor.Where(d => d.IdDoctor == id).Any())
            {
                var doctor = hospitalDbContext.Doctor.SingleOrDefault(d => d.IdDoctor == id);
                return Ok(doctor);
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult AddDoctor(AddDoctor addDoctor)
        {
            if (!(hospitalDbContext.Doctor.Where(d => d.IdDoctor == addDoctor.IdDoctor).Any()))
            {
                var doctor = new Doctor
                {
                    IdDoctor = addDoctor.IdDoctor,
                    FirstName = addDoctor.FirstName,
                    LastName = addDoctor.LastName,
                    Email = addDoctor.Email
                };
                hospitalDbContext.Doctor.Add(doctor);
                hospitalDbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            if (hospitalDbContext.Doctor.Where(d => d.IdDoctor == id).Any())
            {
                var doctor = hospitalDbContext.Doctor.SingleOrDefault(d => d.IdDoctor == id);
                hospitalDbContext.Doctor.Remove(doctor);
                hospitalDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ModifyStudent(ModifyDoctor modifyDoctor)
        {
            if (hospitalDbContext.Doctor.Where(d => d.IdDoctor == modifyDoctor.Id).Any())
            {
                var doctor = hospitalDbContext.Doctor.SingleOrDefault(s => s.IdDoctor == modifyDoctor.Id);
                if (doctor.IdDoctor != Int32.Parse(modifyDoctor.IdDoctor) && modifyDoctor.IdDoctor != null)
                {
                    doctor.IdDoctor = Int32.Parse(modifyDoctor.IdDoctor);
                    hospitalDbContext.SaveChanges();
                }
                if (doctor.FirstName != modifyDoctor.FirstName && modifyDoctor.FirstName != null)
                {
                    doctor.FirstName = modifyDoctor.FirstName;
                    hospitalDbContext.SaveChanges();
                }
                if (doctor.LastName != modifyDoctor.LastName && modifyDoctor.LastName != null)
                {
                    doctor.LastName = modifyDoctor.LastName;
                    hospitalDbContext.SaveChanges();
                }
                if (doctor.Email != modifyDoctor.Email && modifyDoctor.Email != null)
                {
                    doctor.Email = modifyDoctor.Email;
                    hospitalDbContext.SaveChanges();
                }
                return Ok();
            }
            return NotFound();
        }
    }
}