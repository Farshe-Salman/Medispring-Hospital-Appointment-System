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
    internal class DoctorScheduleRepo : IDoctorScheduleFeature
    {
        HDMSContext db;
        public DoctorScheduleRepo( HDMSContext db )
        {
            this.db = db;
        }
        public List<DoctorSchedule> GetByBranch(int bId)
        {
            return db.DoctorSchedules
                .Where(x=> x.BranchId == bId)
                .ToList();
        }

        public List<DoctorSchedule> GetByDoctor(int dId)
        {
            return db.DoctorSchedules
                .Where(x=> x.DoctorId == dId)
                .ToList();
        }

        public DoctorSchedule? GetForDay(int dId, int bId, WeekDay day)
        {
            return db.DoctorSchedules.FirstOrDefault(
                x =>
                x.DoctorId == dId &&
                x.BranchId == bId &&
                x.DayOfWeek == day);
        }

        public bool HasTimeOverlap(int dId, WeekDay day, TimeSpan start, TimeSpan end)
        {
            return db.DoctorSchedules.Any(x =>
            x.DoctorId == dId &&
            x.DayOfWeek == day &&
            x.EndTime > start &&
            x.StartTime < end
            );
        }

        public DoctorSchedule? GetById(int dId)
        {
            return db.DoctorSchedules.Find(dId);
        }
        public bool HasTimeOverlapExceptSelf(int sId, int dId, WeekDay day, TimeSpan start, TimeSpan end)
        {
            return db.DoctorSchedules.Any(x =>
            x.Id != sId &&
            x.DoctorId == dId &&
            x.DayOfWeek == day &&
            x.EndTime > start &&
            x.StartTime < end
            );
        }
    }
}
