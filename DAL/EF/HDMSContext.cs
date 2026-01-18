using DAL.EF.Models;
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
        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.DoctorBranch> DoctorBranches { get; set; }
        public DbSet<Models.DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Models.Patient> Patients { get; set; }
        public DbSet<Models.Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.DoctorSchedule)
                .WithMany()
                .HasForeignKey(a => a.DoctorScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
