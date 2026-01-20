using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration cfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Branch, BranchDTO>().ReverseMap();
            cfg.CreateMap<Branch, AddBranchDTO>().ReverseMap();
            cfg.CreateMap<Doctor, DoctorDTO>().ReverseMap();
            cfg.CreateMap<Doctor, DoctorAppointmentDTO>().ReverseMap();
            cfg.CreateMap<DoctorBranch, DoctorBranchDTO>().ReverseMap();
            cfg.CreateMap<DoctorSchedule, DoctorScheduleDTO>().ReverseMap();
            cfg.CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            cfg.CreateMap<Patient, PatientDTO>().ReverseMap();
            

        });

        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
