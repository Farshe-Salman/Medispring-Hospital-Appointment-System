using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DoctorScheduleDTO
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }


        public int BranchId { get; set; }


        public WeekDay DayOfWeek { get; set; }


        public TimeSpan StartTime { get; set; }


        public TimeSpan EndTime { get; set; }

        public int ConsultationDurationMin { get; set; }

        public string RoomNumber { get; set; }
    }
}
