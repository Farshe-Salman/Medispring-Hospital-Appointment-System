using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DoctorBranchService
    {
        DataAccessFactory factory;

        public DoctorBranchService(DataAccessFactory factory)
        {
            this.factory = factory;
        }

        public ServiceResultDTO AssignDrToBranch(DoctorBranchDTO d)
        {
            var doctor = factory.G_DoctorRepository().Get(d.DoctorId);
            if (doctor == null)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor not found"
                };

            if (!doctor.IsActive)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor is inactive"
                };

            var branch = factory.G_BranchRepository().Get(d.BranchId);
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
                    Message = "Branch is inactive"
                };

            var assigned = factory.S_DoctorBranchRepo()
                .AssignDrToBranch(d.DoctorId, d.BranchId);

            if (!assigned)
                return new ServiceResultDTO
                {
                    Success = false,
                    Message = "Doctor already assigned to this branch"
                };

            return new ServiceResultDTO
            {
                Success = true,
                Message = "Doctor assigned to branch successfully"
            };


        }

        public List<DoctorBranchDTO> GetDoctorsByBranch(int bId)
        {
            var data= factory.S_DoctorBranchRepo().GetDoctorsByBranch(bId);

            return MapperConfig.GetMapper().Map<List<DoctorBranchDTO>>(data);
        }

        public List<DoctorBranchDTO> GetBranchesByDoctor(int dId)
        {
            var data= factory.S_DoctorBranchRepo().GetBranchesByDoctor(dId);
            return MapperConfig.GetMapper().Map<List<DoctorBranchDTO>>(data);
        }
    }
}
