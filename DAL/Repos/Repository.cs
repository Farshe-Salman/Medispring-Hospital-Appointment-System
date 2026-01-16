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
    internal class Repository<T> : IRepository<T> where T : class
    {
        DbSet<T> table;
        HDMSContext db;
        public Repository(HDMSContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public bool Add(T obj)
        {
            table.Add(obj); 
            return db.SaveChanges() > 0;

        }
        
        public List<T> GetAll()
        {
            return table.ToList();
             
        }

        public T? Get(int id)
        {
            return table.Find(id); 
        }


        public bool Update(T obj)
        {
            table.Update(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            table.Remove(ex);
            return db.SaveChanges() > 0;
        }


    }
}
