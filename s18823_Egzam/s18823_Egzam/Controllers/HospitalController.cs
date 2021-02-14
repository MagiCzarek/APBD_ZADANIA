using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s18823_Egzam.Controllers.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace s18823_Egzam.Controllers
{
    public class HospitalController : ControllerBase
    {
        private readonly HospitalDbContext hospitalDbContext;

        public HospitalController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetMedicament(int id)
        {
            if (hospitalDbContext.Medicament.Where(d => d.IdMedicament == id).Any())
            {
                var prescription = hospitalDbContext.Prescription_Medicament
                    .Include(d=>d.Prescription)
                    .Where(d => d.IdMedicament == id).Select(d=>d.Prescription)
                    .OrderBy(d=>d.Date).ToList();
                var medicament = hospitalDbContext.Medicament.SingleOrDefault(d => d.IdMedicament == id);
                medicament.Prescription = prescription;
                return Ok(medicament);
            }
            return NotFound();
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            if (hospitalDbContext.Patient.Where(d => d.IdPatient == id).Any())
            {

                var presc = hospitalDbContext.Prescription.SingleOrDefault(d => d.IdPatient == id);
                var prescmod = hospitalDbContext.Prescription_Medicament.Where(d => d.IdPrescription == presc.IdPrescription).ToList();
                var patient = hospitalDbContext.Patient.SingleOrDefault(d => d.IdPatient == id);
                foreach(var item in prescmod)
                {
                    hospitalDbContext.Prescription_Medicament.Remove(item);
                }
                hospitalDbContext.Prescription.Remove(presc);
                hospitalDbContext.Patient.Remove(patient);
                hospitalDbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        
    }
}