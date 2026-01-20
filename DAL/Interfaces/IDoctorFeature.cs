using System;
using DAL.EF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDoctorFeature
    {
        List<Doctor> GetActiveDoctors();
        //List<Doctor> SearchByName(String Name);
        List<Doctor> Search(string keyword);
        List<Doctor> AllDoctorsWithAppointments();
        Doctor GetDoctorWithAppointments(int id);

        List<Doctor> AllDoctorsWithUpcommingAppointments();
        Doctor GetDoctorWithUpcommingAppointments(int id);

        List<Doctor> AllDoctorsWithUpcommingAppointmentsIndividualBranch(int bId);

        Doctor GetDoctorWithUpcommingAppointmentsIndividualBranch(int dId, int bId);
    }
}
