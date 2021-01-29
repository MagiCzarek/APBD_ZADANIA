using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Models
{
    public class ModifyStudent
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        public string Index { get; set; }

        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public int IdEnrollment { get; set; }
    }
}
