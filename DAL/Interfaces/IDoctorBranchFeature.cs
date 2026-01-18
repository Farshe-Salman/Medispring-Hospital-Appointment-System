using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDoctorBranchFeature
    {
        bool AssignDrToBranch(int dId, int bId);
        List<DoctorBranch> GetDoctorsByBranch(int bId);

        List<DoctorBranch> GetBranchesByDoctor(int dId);

    }
}
