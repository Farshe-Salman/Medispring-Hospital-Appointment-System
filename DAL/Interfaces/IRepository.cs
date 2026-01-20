using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        bool Add(T entity);
        List<T> GetAll();
        T? Get(int id);
        bool Update(T entity);
    }
}
