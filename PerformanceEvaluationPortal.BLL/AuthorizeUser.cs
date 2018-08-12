using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.BLL.Interfaces;

namespace PerformanceEvaluationPortal.BLL
{
    
    public  class AuthorizeService:IAuthorizeService
    {
        private ApplicationDbContext _context;
        public AuthorizeService(ApplicationDbContext context)
        {
            _context = context;
        }
        
       
        public  int CheckAuthorize(string userId)
        {
            bool isManager = false;
            bool isReviewer = false;

            var manager = _context.Users
                 .Include(x => x.UsersIManage)
                 .Where(x => x.Id == userId)
                 .FirstOrDefault();

            if (manager?.UsersIManage.Count() > 0)
                isManager = true;

            isReviewer = _context.Users
               .Include(x => x.UsersIWriteReviewFor)
               .Where(x => x.Id == userId)
               .FirstOrDefault()
               .UsersIWriteReviewFor.Count() > 0;
            //If user is Manager
            //if (isManager)
            //    return 1;
            //if user is Reviewer
            //if (isReviewer)
            //    return 2;
            //if user is Manager and Reviewer
            if (isManager && isReviewer)
                return 3;
            else if (isManager)
                    return 1;
            else if(isReviewer)
                return 2;
            //if user is not Manager or Reviewer
            return 0;
        }


        //public  bool CheckAuthorizeReviewer(string userId)
        //{

        //    bool isReviewer = _context.Users
        //       .Include(x => x.UsersIWriteReviewFor)
        //       .Where(x => x.Id == userId)
        //       .FirstOrDefault()
        //       .UsersIWriteReviewFor.Count() > 0;
        //    if (isReviewer)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
