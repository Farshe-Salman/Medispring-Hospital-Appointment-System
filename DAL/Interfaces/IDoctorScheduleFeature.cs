using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDoctorScheduleFeature
    {
        List<DoctorSchedule> GetByDoctor(int dId);

        List<DoctorSchedule> GetByBranch(int bId);

        DoctorSchedule? GetForDay(int dId, int bId, WeekDay day);

        bool HasTimeOverlap(int dId, WeekDay day, TimeSpan start, TimeSpan end);
        DoctorSchedule? GetById(int dId);
        bool HasTimeOverlapExceptSelf(int sId, int dId, WeekDay day, TimeSpan start, TimeSpan end);

    }
}
