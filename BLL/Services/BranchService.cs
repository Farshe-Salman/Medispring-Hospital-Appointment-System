using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BranchService
    {
        DataAccessFactory factory;
        EmailService email;
        public BranchService(DataAccessFactory factory, EmailService email)
        {
            this.factory = factory;
            this.email = email;
        }


        public bool Add(AddBranchDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var br = mapper.Map<Branch>(b);
            br.IsActive = true;
            return factory.G_BranchRepository().Add(br);
        }

        public List<BranchDTO> GetAll()
        {
            var data= factory.G_BranchRepository().GetAll();
            return MapperConfig.GetMapper().Map<List<BranchDTO>>(data);
        }

        public BranchDTO Get(int id)
        {
            var data= factory.G_BranchRepository().Get(id);
            return MapperConfig.GetMapper().Map<BranchDTO>(data);
        }

        //public BranchDTO Get(string name)
        //{
        //    var data = factory.S_BranchRepo().Get(name);
        //    return MapperConfig.GetMapper().Map<BranchDTO>(data);
        //}

        public List<BranchDTO> Search(string key)
        {
            var data = factory.S_BranchRepo().Search(key);
            return MapperConfig.GetMapper().Map<List<BranchDTO>>(data);
        }

        public bool Update(BranchDTO d)
        {
            var existing = factory.G_BranchRepository().Get(d.Id);
            if (existing == null) return false;

            existing.BranchName = d.BranchName;
            existing.Address = d.Address;
            existing.IsActive = d.IsActive;
            return factory.G_BranchRepository().Update(existing);
        }

        public ServiceResultDTO Deactivate(int id)
        {
            var branch = factory.G_BranchRepository().Get(id);

            if (branch == null)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Branch not found"
                };

            if (!branch.IsActive)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Branch already deactivated"
                };

            branch.IsActive = false;
            bool updated = factory.G_BranchRepository().Update(branch);

            if (!updated)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Branch could not be deactivated"
                };

            var appointments = factory.G_AppointmentRepository()
                .GetAll()
                .Where(a =>
                    a.BranchId == id &&
                    a.AppointmentDate >= DateTime.Today &&
                    (a.Status == AppointmentStatus.Approved ||
                     a.Status == AppointmentStatus.Pending)
                )
                .ToList();

            foreach (var ap in appointments)
            {
                ap.Status = AppointmentStatus.Canceled;
                ap.Comment = "Canceled due to branch deactivation";
                factory.G_AppointmentRepository().Update(ap);

                var patient = factory.G_PatientRepository().Get(ap.PatientId);

                if (patient != null)
                {
                    email.Send(
                        patient.Email,
                        "Appointment Canceled",
                        $"Dear {patient.Name},\n\n" +
                        $"Your appointment on {ap.AppointmentDate:dd-MM-yyyy} " +
                        $"has been canceled because the branch is temporarily unavailable.\n\n" +
                        "Please reschedule your appointment at another branch.\n\n" +
                        "Thank you,\nMedispring Hospital"
                    );
                }
            }

            var schedules = factory.G_DoctorScheduleRepository()
                .GetAll()
                .Where(s => s.BranchId == id && s.IsActive)
                .ToList();

            foreach (var sc in schedules)
            {
                sc.IsActive = false;
                factory.G_DoctorScheduleRepository().Update(sc);
            }


            return new ServiceResultDTO
            {
                Success = true,
                Message = $"Branch deactivated and {appointments.Count} appointments canceled with email notification sent"
            };
        }


        public ServiceResultDTO Activate(int id)
        {
            var doc = factory.G_BranchRepository().Get(id);
            if (doc == null)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Branch not found"
                };
            if (doc.IsActive == true)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Branch Already in Activate action"
                };
            doc.IsActive = true;
            bool update = factory.G_BranchRepository().Update(doc);
            if (update)
                return new ServiceResultDTO
                {
                    Success = true,
                    Message = $"Branch ID no: {id} is Active from now"
                };
            return new ServiceResultDTO
            {
                Success = false,
                Message = $"Branch ID no: {id} can not acivated"
            };

        }

    }
}
