using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class HDMSContext : DbContext
    {
        public HDMSContext(DbContextOptions<HDMSContext> options) : base(options) { }

        public DbSet<Models.Branch> Branches { get; set; }

    }
}
