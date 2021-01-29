using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Models
{
    public class Enrollment
    {
        [Key]
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }

        [ForeignKey("IdStudy")]
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }
    }
}