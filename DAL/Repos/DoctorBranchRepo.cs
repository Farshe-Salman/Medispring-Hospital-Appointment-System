using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class DoctorBranchRepo: IDoctorBranchFeature
    {
        HDMSContext db;

        public DoctorBranchRepo( HDMSContext db )
        {
            this.db = db;
        }

        public bool AssignDrToBranch(int dId, int bId)
        {
            var ex = db.DoctorBranches.Any(x => x.DoctorId == dId && x.BranchId == bId);
            if (ex)
            {
                return false;
            }

            db.DoctorBranches.Add(new DoctorBranch 
            { 
                DoctorId = dId, 
                BranchId = bId 
            });

            return db.SaveChanges() > 0;
        }

        public List<DoctorBranch> GetDoctorsByBranch(int bId)
        {
           return db.DoctorBranches
                .Where(x=> x.BranchId == bId)
                .ToList();
        }

        public List<DoctorBranch> GetBranchesByDoctor(int dId)
        {
            return db.DoctorBranches
                .Where(x => x.DoctorId == dId)
                .ToList();
        }
    }
}
