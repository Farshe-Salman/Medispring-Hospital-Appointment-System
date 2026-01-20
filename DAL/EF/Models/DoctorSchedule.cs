using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        
        [ForeignKey("Branch")]

        public int BranchId { get; set; }

        [Required]

        public WeekDay DayOfWeek { get; set; }

        [Required]

        public TimeSpan StartTime { get; set; }

        [Required]

        public TimeSpan EndTime { get; set; }

        [Required]
        public int ConsultationDurationMin { get; set; }

        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; }

        public bool IsActive { get; set; } = true;


        public virtual Doctor Doctor { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
