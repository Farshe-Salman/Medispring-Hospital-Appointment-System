using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        HDMSContext db;
        public DataAccessFactory(HDMSContext db)
        {
            this.db = db;
        }


        public IBranchFeature BranchData()
        {
            return new BranchRepo(db);
        }

        public IRepository<Branch> RepositoryData()
        {
             return new Repository<Branch>(db);
        }
    }
}
