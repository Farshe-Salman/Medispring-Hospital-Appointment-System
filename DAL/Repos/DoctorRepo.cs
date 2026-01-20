using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class DoctorRepo : IDoctorFeature
    {
        HDMSContext db;

        public DoctorRepo(HDMSContext db)
        {
            this.db = db;
        }

        public List<Doctor> GetActiveDoctors()
        {
            //var list = new List<Doctor>();
            return db.Doctors
                .Where(d => d.IsActive)
                .ToList();

        }

        //public List<Doctor> SearchByName(string name)
        //{
        //    var list = new List<Doctor>();
        //    list = db.Doctors
        //        .Where(d => d.Name == name)
        //        .ToList();
        //    return list;
        //}

        public List<Doctor> Search(string keyword)
        {
            return db.Doctors
                .Where(d =>
                d.Name.Contains(keyword) ||
                d.Specialization.Contains(keyword))
                .ToList();
        }


        public List<Doctor> AllDoctorsWithAppointments()
        {
            var appointments = db.Doctors
                .Include(x => x.Appointments)
                .ToList();
            return appointments;
        }

        public Doctor GetDoctorWithAppointments(int id)
        {
            var appointments = db.Doctors
                .Include(x => x.Appointments)
                .SingleOrDefault(x => x.Id == id);
            return appointments;
        }

        public List<Doctor> AllDoctorsWithUpcommingAppointments()
        {
            var appointments = db.Doctors
                .Include(x => x.Appointments
                .Where(a => a.Status == AppointmentStatus.Approved &&
                a.AppointmentDate >= DateTime.Today
                ))
                .Where(x => x.IsActive)
                .ToList();
            return appointments;
        }

        public Doctor GetDoctorWithUpcommingAppointments(int id)
        {
            var appointments = db.Doctors
                .Include(x => x.Appointments
                .Where(a => a.Status == AppointmentStatus.Approved &&
                a.AppointmentDate >= DateTime.Today
                ))
                .SingleOrDefault(x => x.Id == id);
            return appointments;
        }

        public List<Doctor> AllDoctorsWithUpcommingAppointmentsIndividualBranch(int bId)
        {
            var appointments = db.Doctors
                .Include(x => x.Appointments
                .Where(a => a.Status == AppointmentStatus.Approved &&
                a.BranchId == bId &&
                a.AppointmentDate >= DateTime.Today
                ))
                .Where(x => x.IsActive)
                .ToList();
            return appointments;
        }

        public Doctor GetDoctorWithUpcommingAppointmentsIndividualBranch(int dId, int bId)
        {
            var appointments = db.Doctors
                .Include(x => x.Appointments
                .Where(a => a.Status == AppointmentStatus.Approved &&
                a.BranchId == bId &&
                a.AppointmentDate >= DateTime.Today
                ))
                .SingleOrDefault(x => x.Id == dId);
            return appointments;

        }
    }
}
