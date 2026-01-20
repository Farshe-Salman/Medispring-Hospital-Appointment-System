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
    internal class PatientRepo: IPatientFeature
    {
        HDMSContext db;

        public PatientRepo(HDMSContext db)
        {
            this.db = db;
        }

        public List<Patient> Search(string keyword)
        {
            return db.Patients
                .Where(p=> 
                p.Name.Contains(keyword)||
                p.Email.Contains(keyword)||
                p.PhoneNumber.Contains(keyword))
                .ToList();
        }



        public List<Appointment> GetAppointmentHistory(int pId)
        {
            return db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Branch)
                .Where(a => a.PatientId == pId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

        }

        public List<Appointment> GetUpcomingAppointments(int pId)
        {
            return db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Branch)
                .Where(a=> a.PatientId == pId &&
                a.AppointmentDate>=DateTime.Today &&
                a.Status==AppointmentStatus.Approved)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();
        }

        
    }
}
