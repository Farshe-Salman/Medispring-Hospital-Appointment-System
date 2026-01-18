using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AppointmentDTO
    {
        public int patientId { get; set; }

        public int DoctorId { get; set; }

        public int BranchId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public TimeSpan AppointmentTime { get; set; }
    }
}
