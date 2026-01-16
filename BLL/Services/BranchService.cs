using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public bool Add(BranchDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var data = mapper.Map<Branch>(b);
            return factory.RepositoryData().Add(data);
        }

        public List<BranchDTO> GetAll()
        {
            var data= factory.RepositoryData().GetAll();
            return MapperConfig.GetMapper().Map<List<BranchDTO>>(data);
        }

        public BranchDTO Get(int id)
        {
            var data= factory.RepositoryData().Get(id);
            return MapperConfig.GetMapper().Map<BranchDTO>(data);
        }

        public BranchDTO Get(string name)
        {
            var data = factory.BranchData().Get(name);
            return MapperConfig.GetMapper().Map<BranchDTO>(data);
        }

        public bool Update(BranchDTO b)
        {
            var data= MapperConfig.GetMapper().Map<Branch>(b);
            return factory.RepositoryData().Update(data);
        }

        public bool Delete(int id)
        {
            return factory.RepositoryData().Delete(id);
        }

    }
}
