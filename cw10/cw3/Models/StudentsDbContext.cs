using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Models
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Studies> Studies { get; set; }


        public StudentsDbContext() { }

        public StudentsDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        internal void ExecuteCommand(string v)
        {
            throw new NotImplementedException();
        }
    }
}
