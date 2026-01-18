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
        public BranchService(DataAccessFactory factory)
        {
            this.factory = factory;
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

        public BranchDTO Get(string name)
        {
            var data = factory.S_BranchRepo().Get(name);
            return MapperConfig.GetMapper().Map<BranchDTO>(data);
        }

        public bool Update(BranchDTO b)
        {
            var data= MapperConfig.GetMapper().Map<Branch>(b);
            return factory.G_BranchRepository().Update(data);

            var existing = factory.G_BranchRepository().Get(b.Id);
            if (existing == null) return false;

            existing.BranchName = b.BranchName;
            existing.Address = b.Address;

            return factory.G_BranchRepository().Update(existing);
        }

        public bool Deactivate(int id)
        {
            var b = factory.G_BranchRepository().Get(id);
            if (b == null) return false;

            b.IsActive = false;
            return factory.G_BranchRepository().Update(b);
        }

    }
}
