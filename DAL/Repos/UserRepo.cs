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
    internal class UserRepo: IUserFeature
    {
        HDMSContext db;

        public UserRepo(HDMSContext db)
        {
            this.db = db;
        }

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(u=>u.UserName==userName);
        }
    }
}
