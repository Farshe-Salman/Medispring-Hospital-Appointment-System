using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DoctorScheduleUpdateDto
    {
        public int Id { get; set; }
        public WeekDay DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int ConsultationDurationMin { get; set; }

        public string RoomNumber { get; set; }

    }
}
