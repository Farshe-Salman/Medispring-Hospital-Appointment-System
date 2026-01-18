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

        //For Branch
        public IRepository<Branch> G_BranchRepository()
        {
            return new Repository<Branch>(db);
        }

        public IBranchFeature S_BranchRepo()
        {
            return new BranchRepo(db);
        }

        //For doctor
        public IRepository<Doctor> G_DoctorRepository()
        {
            return new Repository<Doctor>(db);
        }

        public IDoctorFeature S_DoctorRepo()
        {
            return new DoctorRepo(db); 
        }

        //For Doctor Branch
        public IDoctorBranchFeature S_DoctorBranchRepo()
        {
            return new DoctorBranchRepo(db);
        }


        //For DoctorSchedule
        public IRepository<DoctorSchedule> G_DoctorScheduleRepository()
        {
            return new Repository<DoctorSchedule>(db);
        }
        public IDoctorScheduleFeature S_DoctorScheduleRepo()
        {
            return new DoctorScheduleRepo(db);
        }
    }
}
