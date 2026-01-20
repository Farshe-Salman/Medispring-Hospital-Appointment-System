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
    public class AppointmentService
    {
        DataAccessFactory factory;
        EmailService email;

        public AppointmentService(DataAccessFactory factory, EmailService email)
        {
            this.factory = factory;
            this.email = email;
        }

        public ServiceResultDTO Book(AppointmentDTO dto)
        {
            var patient = factory.G_PatientRepository().Get(dto.PatientId);

            if(patient == null || !patient.IsActive)
            {
                return new ServiceResultDTO()
                {
                    Success = false,
                    Message = "Patient not available or not Inactive"
                };
            }

            var doctor = factory.G_DoctorRepository().Get(dto.DoctorId);
            if (doctor == null || !doctor.IsActive)
            {
                return new ServiceResultDTO 
                {
                    Success = false, 
                    Message = "Doctor unavailable" 
                };
            }
                

            var branch = factory.G_BranchRepository().Get(dto.BranchId);
            if (branch == null || !branch.IsActive)
            {
                return new ServiceResultDTO 
                { 
                    Success = false, 
                    Message = "Branch unavailable" 
                };
            }

            var day = (WeekDay)dto.AppointmentDate.DayOfWeek;

            var schedule = factory.S_DoctorScheduleRepo().GetForDay(dto.DoctorId,dto.BranchId, day, dto.AppointmentTime);

            if(schedule == null)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "No Schedule on this day/time For this Doctor/branch"
                };
            }

            if(schedule.IsActive==false)
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "This Schdule is not active"
                };
            }

            //if(dto.AppointmentTime < schedule.StartTime || dto.AppointmentTime >= schedule.EndTime)
            //{
            //    return new ServiceResultDTO
            //    {
            //        Success = false,
            //        Message = "Appointment time not in schedule"
            //    };
            //}

            var totalMin = (schedule.EndTime - schedule.StartTime).TotalMinutes;
            var maxConsultation = (int)(totalMin / schedule.ConsultationDurationMin);

            var approvedConsultation = factory.S_AppointmentRepo().CountApproved(dto.DoctorId, dto.AppointmentDate);

            var status = approvedConsultation < maxConsultation ? AppointmentStatus.Approved : AppointmentStatus.Rejected;


            var serial = factory.S_AppointmentRepo().GetMaxSerial(dto.DoctorId, dto.AppointmentDate) + 1;

            var ap = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                BranchId = dto.BranchId,
                DoctorScheduleId = schedule.Id,
                AppointmentDate = dto.AppointmentDate.Date,
                AppointmentTime = dto.AppointmentTime,
                Status = status,
                SerialNumber = serial,
                Comment = status == AppointmentStatus.Approved
                    ? null
                    : "Daily limit exceeded"
            };

            string SerialTime = DateTime.Today.Add(schedule.StartTime.Add(TimeSpan.FromMinutes((serial - 1) * schedule.ConsultationDurationMin))).ToString("hh:mm tt");
                
            factory.G_AppointmentRepository().Add(ap);

            if (status == AppointmentStatus.Approved)
            {
                email.Send(
                    patient.Email,
                    $"Appointment Approved at {SerialTime} In {dto.AppointmentDate:dd-MM-yyyy}",
                    $"MediSpring Hospital\nYour appointment is approved.\nPatient Name: {patient.Name}\nDoctor Name: {doctor.Name}\nSerial: {serial}\nSerial Time: {SerialTime}\nDate: {dto.AppointmentDate:dd-MM-yyyy}\n\n\nThank you\nFor any query contact with us via: \nPhone: 01319946481 \nEmail: info@medispring.com"
                    );
            }
            else
            {
                email.Send(
                    patient.Email,
                    "Appointment Rejected",
                    "Daily Patient Limit Exceed"
                    );
            }

            return new ServiceResultDTO
            {
                Success = status == AppointmentStatus.Approved,
                Message = status == AppointmentStatus.Approved
                    ? $"Approved. Serial: {serial}"
                    : "Rejected. Email sent."
            };

        }

        public List<FullAppointmentDTO> AppointmentList()
        {
            var data = factory.G_AppointmentRepository().GetAll();
            return data.Select(x => new FullAppointmentDTO
            {
                Id = x.Id,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                BranchId = x.BranchId,
                DoctorScheduleId = x.DoctorScheduleId,
                AppointmentDate = x.AppointmentDate,
                AppointmentTime = x.AppointmentTime,
                Status = x.Status
            }).ToList();
        }

        public ServiceResultDTO Cancel(int id, string reason)
        {
            var ap = factory.G_AppointmentRepository().Get(id);
            if ( ap == null )
            {
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Not found"
                };
            }


            ap.Status = AppointmentStatus.Canceled;
            ap.Comment = reason;
            factory.G_AppointmentRepository().Update( ap ); 

            var patient = factory.G_PatientRepository().Get(ap.PatientId);
            var doctor = factory.G_DoctorRepository().Get(ap.DoctorId);

            email.Send(
                patient.Email,
                "Appointment Cancelled",
                $"\nDear Sir,\nPatinet Name: {patient.Name}\nDoctor Name: {doctor.Name}\nDate: {ap.AppointmentDate} \nWe are very sorry, Your Appointment is Cancelled \nReason: {reason}\n\n\nThank you\nFor any query contact with us via: \nPhone: 01319946481 \nEmail: info@medispring.com"
                );

            return new ServiceResultDTO
            {
                Success = true,
                Message = "Cancelled Appointment"
            };

        }


        public List<AppointmentReportDTO> GetDailyReport(DateTime date)
        {
            var data = factory.S_AppointmentRepo().GetDailyAppointment(date);

            return data
                .GroupBy(a => a.AppointmentDate.Date)
                .Select(g => new AppointmentReportDTO
                {
                    Date = g.Key,
                    TotalAppointments = g.Count(),
                    Approved = g.Count(a => a.Status == AppointmentStatus.Approved),
                    Rejected = g.Count(a => a.Status == AppointmentStatus.Rejected),
                    Cancelled = g.Count(a => a.Status == AppointmentStatus.Canceled)
                })
                .ToList();
        }



    }
}
