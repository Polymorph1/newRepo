using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DAL;
using System.Data.Entity;
using PerformanceEvaluationPortal.Common.Mappers;
using PerformanceEvaluationPortal.Common.Validation;


namespace PerformanceEvaluationPortal.BLL
{
    public class ApplicationUserService : IApplicationUserService
    {
        
        private ApplicationDbContext _context;
        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public ICollection<ApplicationUserModel> GetMyTeam(string managerId)
        {
            if (String.IsNullOrWhiteSpace(managerId))
                throw new ArgumentOutOfRangeException("managerId", "Id can't be null.");

             var user = _context.Users
                                        .Include(x => x.JobTitle)
                                        .Include(x => x.JobPosition)
                                        .Where(x => x.ManagerId == managerId)
                                        .ToList();

                return user.Select(x => x.MapApplicationUserToApplicationUserModel(true, true, false, false, false))
                            .ToList();  
        }

        public ApplicationUserModel GetLoggedUser(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                throw new ArgumentOutOfRangeException("userId", "Id can't be null.");

            var user = _context.Users.Include(x => x.UsersIManage)
                                    .Include(x => x.UsersIWriteReviewFor)
                                    .Include(x => x.JobTitle)
                                    .Include(x => x.JobPosition)
                                    .Include(x => x.Manager)
                                    .Include(x => x.Reviewer)
                                    .Include(x => x.Template)
                                    .Where(x => x.Id == userId)
                                    .FirstOrDefault();

            if (user == null)
                throw new ApplicationException (String.Format("User with id {0} does not exist", userId));

            return user.MapApplicationUserToApplicationUserModel();
        }

        public ICollection<ApplicationUserModel> GetMyReviewees(string reviewerId)
        {
            if (String.IsNullOrWhiteSpace(reviewerId))
                throw new ArgumentOutOfRangeException("reviewerId", "Id can't be null.");

            var user = _context.Users
                                    .Include(x => x.JobTitle)
                                    .Include(x => x.JobPosition)
                                    .Where(x => x.ReviewerId == reviewerId)
                                    .ToList();

            return user.Select(x => x.MapApplicationUserToApplicationUserModel(true, true, false, false, false))
                        .ToList();
        }

        public ApplicationUserModel GetUser(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                throw new ArgumentOutOfRangeException("userId", "Id can't be null.");

            var user = _context.Users.Where(x => x.Id == userId)
                                    .Include(x => x.UsersIManage)
                                    .Include(x => x.UsersIWriteReviewFor)
                                    .Include(x => x.JobTitle)
                                    .Include(x => x.JobPosition)
                                    .Include(x => x.Manager)
                                    .Include(x => x.Reviewer)
                                    .Include(x => x.Template)                                    
                                    .FirstOrDefault();

            if (user == null)
                throw new ApplicationException(String.Format("User with id {0} does not exist", userId));

            return user.MapApplicationUserToApplicationUserModel();
        }

        public ICollection<ApplicationUserModel> GetUsers()
        {
            
            var users = _context.Users.Include(x => x.JobTitle)
                                    .Include(x => x.JobPosition)                                    
                                    .ToList()
                                    .OrderBy(x=>x.FirstName);

            
            return users.Select(x => x.MapApplicationUserToApplicationUserModel(true, true, false, false, false))
                        .ToList();
        }

        public ApplicationUserModel UpdateUser(string userId, ApplicationUserModel userModel)
        {
            
            List<string> errorMessage = null;
            if (!userModel.IsValid(out errorMessage))
            {
                throw new ArgumentException("userModel", String.Join("; ", errorMessage));
            }

            var user = _context.Users
                                .Where(x => x.Id == userId)
                                .FirstOrDefault();

            if (user == null)
                throw new ApplicationException(String.Format("User with id {0} does not exist", userModel.Id));

            user = userModel.MapApplicationUserModelToApplicationUser(user);

            _context.SaveChanges();
            return user.MapApplicationUserToApplicationUserModel();
        }
        

        public ApplicationUserModel GetUserByUsername(string username)
        {
            var user = _context.Users.Include(x => x.UsersIManage)
                                     .Include(x => x.UsersIWriteReviewFor)
                                     .Include(x => x.JobTitle)
                                     .Include(x => x.JobPosition)
                                     .Where(x => x.UserName == username)
                                     .FirstOrDefault();
            if (user == null)
                throw new ApplicationException(String.Format("User with username {0} does not exist", username));

            return user.MapApplicationUserToApplicationUserModel(true,true,false,false,false);
        }
    }
}


