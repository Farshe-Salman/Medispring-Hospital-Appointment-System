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
    internal class DoctorRepo : IDoctorFeature
    {
        HDMSContext db;

        public DoctorRepo(HDMSContext db)
        {
            this.db = db; 
        }

        public List<Doctor> GetActiveDoctors()
        {
            //var list = new List<Doctor>();
            return db.Doctors
                .Where(d => d.IsActive)
                .ToList();
           
        }

        public List<Doctor> SearchByName(string name)
        {
            var list = new List<Doctor>();
            list=db.Doctors
                .Where (d => d.Name == name)
                .ToList();
            return list;
        }

    }
}
