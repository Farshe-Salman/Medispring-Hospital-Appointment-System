using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AppointmentRepo: IAppointmentFeature
    {
        HDMSContext db;

        public AppointmentRepo(HDMSContext db)
        {
            this.db = db;
        }

        public int CountApproved(int dId, DateTime date)
        {
            return db.Appointments.Count(x =>
            x.DoctorId == dId &&
            x.AppointmentDate.Date == date.Date &&
            x.Status == AppointmentStatus.Approved
            );
        }

        public int GetMaxSerial(int doctorId, DateTime date)
        {
            var s = db.Appointments
                .Where(x => x.DoctorId == doctorId && x.AppointmentDate.Date == date.Date)
                .Select(a => a.SerialNumber);

            return s.Any() ? s.Max() : 0;
        }

    }
}
