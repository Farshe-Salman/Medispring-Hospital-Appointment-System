using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    [Index(nameof(DoctorId), nameof(AppointmentDate))]
    public class Appointment
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [ForeignKey("DoctorSchedule")]
        public int DoctorScheduleId { get; set; }
        [Required]
        public DateTime AppointmentDate {  get; set; }
        [Required]
        public TimeSpan AppointmentTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public int SerialNumber { get; set; }
        public string? Comment { get; set; }             //Only for rejection
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual DoctorSchedule DoctorSchedule { get; set; }

        


    }
}
