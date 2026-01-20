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
    public class DoctorService
    {
        DataAccessFactory factory;
        EmailService email;
        public DoctorService(DataAccessFactory factory, EmailService email)
        {
            this.factory = factory;
            this.email = email;
        }

        public bool Add(DoctorDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var doctor = mapper.Map<Doctor>(b);
            doctor.IsActive=true;
            return factory.G_DoctorRepository().Add(doctor);
        }

        public List<DoctorDTO> GetAll()
        {
            var data = factory.G_DoctorRepository().GetAll();
            return MapperConfig.GetMapper().Map<List<DoctorDTO>>(data);
        }

        public DoctorDTO Get(int id)
        {
            var data = factory.G_DoctorRepository().Get(id);
            return MapperConfig.GetMapper().Map<DoctorDTO>(data);
        }


        public List<DoctorDTO> Search(string key)
        {
            var data = factory.S_DoctorRepo().Search(key);
            return MapperConfig.GetMapper().Map<List<DoctorDTO>>(data);
        }


        public List<DoctorDTO> GetActiveDoctors()
        {
            var data = factory.S_DoctorRepo().GetActiveDoctors();
            return MapperConfig.GetMapper().Map<List<DoctorDTO>>(data);
        }

        public bool Update(DoctorDTO d)
        {
            var existing = factory.G_DoctorRepository().Get(d.Id);
            if (existing == null) return false;

            existing.Name = d.Name;
            existing.Specialization = d.Specialization;
            existing.IsActive = d.IsActive;
            return factory.G_DoctorRepository().Update(existing);
        }

        public ServiceResultDTO Deactivate(int id)
        {
            var doc = factory.G_DoctorRepository().Get(id);

            if (doc == null)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor not found"
                };

            if (doc.IsActive == false)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor already deactivated"
                };

            doc.IsActive = false;
            bool updated = factory.G_DoctorRepository().Update(doc);

            if (updated == false)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor could not be deactivated"
                };

            var appointments = factory.G_AppointmentRepository().GetAll()
                .Where(a =>
                    a.DoctorId == id &&
                    a.AppointmentDate >= DateTime.Today &&
                    (a.Status == AppointmentStatus.Approved ||
                     a.Status == AppointmentStatus.Pending)
                )
                .ToList();

            foreach (var ap in appointments)
            {
                ap.Status = AppointmentStatus.Canceled;
                ap.Comment = "Canceled due to doctor deactivation";
                factory.G_AppointmentRepository().Update(ap);

                var patient = factory.G_PatientRepository().Get(ap.PatientId);

                if (patient != null)
                {
                    email.Send(
                        patient.Email,
                        "Appointment Canceled",
                        $"Dear {patient.Name},\n\n" +
                        $"Your appointment with Dr. {doc.Name} on " +
                        $"{ap.AppointmentDate:dd-MM-yyyy} has been canceled " +
                        $"{ap.Comment}\n\n" +
                        "Please reschedule your appointment.\n\n" +
                        "Thank you,\nMedispring Hospital"
                    );
                }
            }

            var schedules = factory.G_DoctorScheduleRepository()
                .GetAll()
                .Where(s => s.DoctorId == id && s.IsActive)
                .ToList();

            foreach (var sc in schedules)
            {
                sc.IsActive = false;
                factory.G_DoctorScheduleRepository().Update(sc);
            }


            return new ServiceResultDTO
            {
                Success = true,
                Message = $"Doctor deactivated and {appointments.Count} appointments canceled"
            };
        }


        public ServiceResultDTO Activate(int id)
        {
            var doc = factory.G_DoctorRepository().Get(id);
            if (doc == null)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor not found"
                };
            if (doc.IsActive == true)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor Already in Activate action"
                };
            doc.IsActive = true;
            bool update = factory.G_DoctorRepository().Update(doc);
            if (update)
                return new ServiceResultDTO
                {
                    Success = true,
                    Message = $"Doctor ID no: {id} is Active from now"
                };
            return new ServiceResultDTO
            {
                Success = false,
                Message = $"Doctor ID no: {id} can not acivated"
            };
        }

        public List<DoctorAppointmentDTO> AllDoctorsWithAppointments()
        {
            var data = factory.S_DoctorRepo().AllDoctorsWithAppointments();
            return MapperConfig.GetMapper().Map<List<DoctorAppointmentDTO>>(data);

        }

        public DoctorAppointmentDTO GetDoctorWithAppointments(int id)
        {
            var data = factory.S_DoctorRepo().GetDoctorWithAppointments(id);
            return MapperConfig.GetMapper().Map<DoctorAppointmentDTO>(data);
        }

        public List<DoctorAppointmentDTO> AllDoctorsWithUpcommingAppointments()
        {
            var data = factory.S_DoctorRepo().AllDoctorsWithUpcommingAppointments();
            return MapperConfig.GetMapper().Map<List<DoctorAppointmentDTO>>(data);

        }

        public DoctorAppointmentDTO GetDoctorWithUpcommingAppointments(int id)
        {
            var data = factory.S_DoctorRepo().GetDoctorWithUpcommingAppointments(id);
            return MapperConfig.GetMapper().Map<DoctorAppointmentDTO>(data);
        }

        public List<DoctorAppointmentDTO> AllDoctorsWithUpcommingAppointmentsIndividualBranch(int bId)
        {
            var data = factory.S_DoctorRepo().AllDoctorsWithUpcommingAppointmentsIndividualBranch(bId);
            return MapperConfig.GetMapper().Map<List<DoctorAppointmentDTO>>(data);

        }

        public DoctorAppointmentDTO GetDoctorWithUpcommingAppointmentsIndividualBranch(int dId, int bId)
        {
            var data = factory.S_DoctorRepo().GetDoctorWithUpcommingAppointmentsIndividualBranch(dId, bId);
            return MapperConfig.GetMapper().Map<DoctorAppointmentDTO>(data);
        }

    }
}
