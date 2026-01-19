using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAppointmentFeature
    {
        int CountApproved(int doctorId, DateTime date);
        int GetMaxSerial(int doctorId, DateTime date);

    }
}
