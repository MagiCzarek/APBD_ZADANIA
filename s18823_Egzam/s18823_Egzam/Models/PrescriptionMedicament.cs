using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace s18823_Egzam.Controllers.Models
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public virtual Prescription Prescription{ get; set;}
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }
    }
}
