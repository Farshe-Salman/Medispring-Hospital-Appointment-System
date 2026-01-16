using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    [Index(nameof(BranchName), IsUnique = true)]
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set;  }
        [Required]
        [StringLength(200)]

        public string Address { get; set; }


    }
}
