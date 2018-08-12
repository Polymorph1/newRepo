using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.BLL.Interfaces
{
    public interface IApplicationUserService
    {
        ICollection<ApplicationUserModel> GetUsers();
        ApplicationUserModel GetUserByUsername(string username);
        ApplicationUserModel UpdateUser(string userId,ApplicationUserModel userModel);
        ApplicationUserModel GetUser(string userId);
        ICollection<ApplicationUserModel> GetMyReviewees(string reviewerId);
        ICollection<ApplicationUserModel> GetMyTeam(string managerId);
        ApplicationUserModel GetLoggedUser(string userId);
    }
}
