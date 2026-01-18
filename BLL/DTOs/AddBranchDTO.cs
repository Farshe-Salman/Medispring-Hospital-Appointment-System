using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AddBranchDTO
    {
        public int Id { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }
    }
}
