using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PatientService
    {
        DataAccessFactory factory;

        public PatientService(DataAccessFactory factory)
        {
            this.factory = factory;
        }

        public bool Add(PatientDTO dto)
        {
            var patient = MapperConfig.GetMapper().Map<Patient>(dto);
            
            patient.IsActive = true;

            return factory.G_PatientRepository().Add(patient);
        }

        public List<PatientDTO> GetAll()
        {
            var data = factory.G_PatientRepository().GetAll();
            return MapperConfig.GetMapper().Map<List<PatientDTO>>(data);
        }

        public PatientDTO Get(int id)
        {
            var data = factory.G_PatientRepository().Get(id);
            return MapperConfig.GetMapper().Map<PatientDTO>(data);
        }

        public List<PatientDTO> Search(string key)
        {
            var data = factory.S_PatientRepo().Search(key);
            return MapperConfig.GetMapper().Map<List<PatientDTO>>(data);
        }

        public List<PatientAppointmentDTO> GetAppointHistory(int pId)
        {
            var data = factory.S_PatientRepo().GetAppointmentHistory(pId);

            return data.Select(a=> 
            new PatientAppointmentDTO{
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                DoctorName = a.Doctor.Name,
                BranchName = a.Branch.BranchName,
                Status = a.Status
            }).ToList();
        }

        public List<PatientAppointmentDTO> GetUpcomingAppointments(int pId)
        {
            var data = factory.S_PatientRepo().GetUpcomingAppointments(pId);

            return data.Select(a =>
            new PatientAppointmentDTO
            {
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                DoctorName = a.Doctor.Name,
                BranchName = a.Branch.BranchName,
                Status = a.Status
            }).ToList();
        }




        //Deactivate




    }
}
