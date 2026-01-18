using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class DoctorBranch
    {
        public int Id { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }


        public virtual Doctor Doctor { get; set; }
        public virtual Branch Branch { get; set; }


    }
}
