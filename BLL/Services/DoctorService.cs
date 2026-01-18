using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DoctorService
    {
        DataAccessFactory factory;
        public DoctorService(DataAccessFactory factory)
        {
            this.factory = factory;
        }

        public bool Add(DoctorDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var doctor = mapper.Map<Doctor>(b);
            doctor.IsActive=true;
            return factory.G_DoctorRepository().Add(doctor);
        }

        public List<DoctorDTO> GetAll()
        {
            var data = factory.G_DoctorRepository().GetAll();
            return MapperConfig.GetMapper().Map<List<DoctorDTO>>(data);
        }

        public DoctorDTO Get(int id)
        {
            var data = factory.G_DoctorRepository().Get(id);
            return MapperConfig.GetMapper().Map<DoctorDTO>(data);
        }

        public List<DoctorDTO> SearchByName(string name)
        {
            var data = factory.S_DoctorRepo().SearchByName(name);
            return MapperConfig.GetMapper().Map<List<DoctorDTO>>(data);
        }

        public List<DoctorDTO> GetActiveDoctors()
        {
            var data = factory.S_DoctorRepo().GetActiveDoctors();
            return MapperConfig.GetMapper().Map<List<DoctorDTO>>(data);
        }

        public bool Update(DoctorDTO d)
        {
            var existing = factory.G_DoctorRepository().Get(d.Id);
            if (existing == null) return false;

            existing.Name = d.Name;
            existing.Specialization = d.Specialization;

            return factory.G_DoctorRepository().Update(existing);
        }

        public bool Deactivate(int id)
        {
            var doc = factory.G_DoctorRepository().Get(id);
            if (doc == null) return false;

            doc.IsActive = false;
            return factory.G_DoctorRepository().Update(doc);
        }

    }
}
