using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BLL.Services
{
    public class DoctorScheduleService
    {
        DataAccessFactory factory;
        EmailService email;

        public DoctorScheduleService(DataAccessFactory factory, EmailService email)
        {
            this.factory = factory;
            this.email = email;
        }

        public ServiceResultDTO Add(DoctorScheduleDTO dto)
        {
            var doctor = factory.G_DoctorRepository().Get(dto.DoctorId);
            if (doctor == null)
                return new ServiceResultDTO { Success = false, Message = "Doctor not found" };

            if (!doctor.IsActive)
                return new ServiceResultDTO { Success = false, Message = "Doctor is inactive" };

            
            var branch = factory.G_BranchRepository().Get(dto.BranchId);
            if (branch == null)
                return new ServiceResultDTO { Success = false, Message = "Branch not found" };

            if (!branch.IsActive)
                return new ServiceResultDTO { Success = false, Message = "Branch is inactive" };

            if (dto.StartTime >= dto.EndTime)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Start time must be earlier than end time"
                };

            bool overlap = factory.S_DoctorScheduleRepo().HasTimeOverlap(dto.DoctorId, dto.DayOfWeek, dto.StartTime, dto.EndTime);
            if (overlap)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "This doctor scheduled is clash"
                };
            }


            if (dto.ConsultationDurationMin <= 0)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Consultation duration must be greater than 0"
                };
            }

            var totalMinutes = (dto.EndTime - dto.StartTime).TotalMinutes;
            var maxPatients = (int)(totalMinutes / dto.ConsultationDurationMin);

            if (maxPatients <= 0)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Schedule time too short for any patient"
                };

            var sc = MapperConfig.GetMapper().Map<DoctorSchedule>(dto);
            factory.G_DoctorScheduleRepository().Add(sc);

            return new ServiceResultDTO
            {
                Success = true,
                Message = $"Schedule added successfully (Max patients/day: {maxPatients})"
            };

        }
        public List<DoctorScheduleDTO> GetByDoctor(int doctorId)
        {
            var data = factory.S_DoctorScheduleRepo().GetByDoctor(doctorId);
            return MapperConfig.GetMapper().Map<List<DoctorScheduleDTO>>(data);
        }

        public List<DoctorScheduleDTO> GetByBranch(int branchId)
        {
            var data = factory.S_DoctorScheduleRepo().GetByBranch(branchId);
            return MapperConfig.GetMapper().Map<List<DoctorScheduleDTO>>(data);
        }

        public ServiceResultDTO Update(DoctorScheduleUpdateDto dto)
        {
            var schedule = factory.S_DoctorScheduleRepo().GetById(dto.Id);
            if(schedule == null)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = " Schedule not found"
                };
            }

            if(dto.StartTime >= dto.EndTime)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Start time must be earlier than end time"
                };
            }

            if(dto.ConsultationDurationMin <= 0)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Consultation duration must be greater than 0"
                };
            }

            var overlap = factory.S_DoctorScheduleRepo().HasTimeOverlapExceptSelf(schedule.Id, schedule.DoctorId, dto.DayOfWeek, dto.StartTime, dto.EndTime);

            if(overlap)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Updated Schedule Overlap with another schedule"
                };
            }

            schedule.DayOfWeek = dto.DayOfWeek;
            schedule.StartTime = dto.StartTime;
            schedule.EndTime = dto.EndTime;
            schedule.ConsultationDurationMin = dto.ConsultationDurationMin;
            schedule.RoomNumber = dto.RoomNumber;

            bool updated = factory.G_DoctorScheduleRepository().Update(schedule);

            if (!updated)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Schedule not updated"
                };
            }

            return new ServiceResultDTO
            {
                Success = true,
                Message = "Schedule updated successfully"
            };


        }

        public ServiceResultDTO DeactivateSchedule(int id)
        {
            var schedule = factory.G_DoctorScheduleRepository().Get(id);

            if (schedule == null)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor schedule not found"
                };

            if (!schedule.IsActive)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor schedule already deactivated"
                };

            schedule.IsActive = false;
            bool updated = factory.G_DoctorScheduleRepository().Update(schedule);

            if (!updated)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Schedule could not be deactivated"
                };

            var appointments = factory.G_AppointmentRepository()
                .GetAll()
                .Where(a =>
                    a.DoctorScheduleId == id &&
                    a.AppointmentDate >= DateTime.Today &&
                    (a.Status == AppointmentStatus.Approved ||
                     a.Status == AppointmentStatus.Pending)
                )
                .ToList();

            foreach (var ap in appointments)
            {
                ap.Status = AppointmentStatus.Canceled;
                ap.Comment = "Canceled due to schedule deactivation";
                factory.G_AppointmentRepository().Update(ap);

                var patient = factory.G_PatientRepository().Get(ap.PatientId);

                if (patient != null)
                {
                    email.Send(
                        patient.Email,
                        "Appointment Canceled",
                        $"Dear {patient.Name},\n\n" +
                        $"Your appointment on {ap.AppointmentDate:dd-MM-yyyy} " +
                        $"has been canceled due to schedule changes.\n\n" +
                        "Please reschedule your appointment.\n\n" +
                        "Medispring Hospital"
                    );
                }
            }

            return new ServiceResultDTO
            {
                Success = true,
                Message = $"Schedule deactivated and {appointments.Count} appointments canceled"
            };
        }

    }
}
