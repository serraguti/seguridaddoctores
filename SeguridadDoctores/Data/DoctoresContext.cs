using Microsoft.EntityFrameworkCore;
using SeguridadDoctores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadDoctores.Data
{
    public class DoctoresContext: DbContext
    {
        public DoctoresContext(DbContextOptions<DoctoresContext> options)
            : base(options) { }
        public DbSet<Doctor> Doctores { get; set; }
    }
}
