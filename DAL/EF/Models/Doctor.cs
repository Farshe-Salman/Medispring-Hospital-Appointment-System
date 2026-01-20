using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]

        public string Specialization { get; set; }

        public double Fee { get; set; }

        public bool IsActive { get; set; }=true;

        public virtual List<DoctorBranch> DoctorBranches { get; set; }

        public virtual List<Appointment> Appointments { get; set; }

        public Doctor()
        {
            Appointments = new List<Appointment>();
        }
    }
}
