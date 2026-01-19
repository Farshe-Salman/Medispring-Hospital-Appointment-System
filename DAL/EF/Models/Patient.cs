using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        [StringLength(14)]

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }=true;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
