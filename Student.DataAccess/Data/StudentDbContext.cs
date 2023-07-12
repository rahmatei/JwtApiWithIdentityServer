using Microsoft.EntityFrameworkCore;
using Student.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataAccess.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=StudentDb; User Id=sa; Password=12345678;  TrustServerCertificate=True");
        }
        public DbSet<StudentModel> Students { get; set; }
    }
}
