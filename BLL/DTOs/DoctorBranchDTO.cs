using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class DoctorBranchDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int BranchId { get; set; }
    }
}
