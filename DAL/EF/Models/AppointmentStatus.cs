using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public enum AppointmentStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Canceled = 3
    }
}
