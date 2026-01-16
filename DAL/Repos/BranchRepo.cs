using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class BranchRepo : IBranchFeature
    {
        HDMSContext db;
        public BranchRepo(HDMSContext db)
        {
            this.db= db;
        }

        //public bool Add(Branch branch)
        //{
        //    db.Branches.Add(branch);
        //    return db.SaveChanges() > 0;
        //}

        //public List<Branch> GetAll()
        //{
        //    return db.Branches.ToList();
        //}

        //public Branch Get(int id)
        //{
        //    return db.Branches.Find(id);
        //}

        public Branch Get(string name)
        {
            return db.Branches.FirstOrDefault(b => b.BranchName == name);
        }

        //public bool Update(Branch branch)
        //{
        //    //var ex= Get(branch.Id);
        //    //db.Entry(ex).CurrentValues.SetValues(branch);
        //    //return db.SaveChanges() > 0;
        //    db.Branches.Update(branch);
        //    return db.SaveChanges() > 0;
        //}

        //public bool Delete(int id)
        //{
        //    var ex = Get(id);
        //    db.Remove(ex);
        //    return db.SaveChanges() > 0;
        //}
    }
}
