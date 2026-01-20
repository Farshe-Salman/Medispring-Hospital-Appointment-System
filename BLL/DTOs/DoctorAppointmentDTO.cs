using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DoctorAppointmentDTO : DoctorDTO
    {
        public List<AppointmentDTO> Appointments { get; set; }

        public DoctorAppointmentDTO()
        {
            Appointments = new List<AppointmentDTO>();
        }
    }
}
