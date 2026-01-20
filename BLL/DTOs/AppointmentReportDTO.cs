using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AppointmentReportDTO
    {
        public DateTime Date {  get; set; }
        public int TotalAppointments { get; set; }
        public int Approved {get; set;}
        public int Rejected {get; set;}
        public int Cancelled {get; set;}

    }
}
