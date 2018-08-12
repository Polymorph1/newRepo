using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class ApplicationUserMapper
    {
        public static ApplicationUserModel MapApplicationUserToApplicationUserModel(this ApplicationUser user, bool mapTitle = true, bool mapPosition = true,
            bool mapManager = true, bool mapReviewer = true, bool mapTemplate = true, bool mapSkills = false)
        {
            if (user == null)
                return null;
            var applicationUserModel = new ApplicationUserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JobPositionId = user.JobPositionId,
                JobTitleId = user.JobTitleId,
                ManagerId = user.ManagerId,
                ReviewerId = user.ReviewerId,
                TemplateId = user.TemplateId,
                EmploymentDate = user.EmploymentDate,
                Username = user.UserName
            };

            applicationUserModel.IsManager = user.UsersIManage != null && user.UsersIManage.Count > 0;

            applicationUserModel.IsReviewer = user.UsersIWriteReviewFor != null && user.UsersIWriteReviewFor.Count > 0;

            if (mapPosition)
                applicationUserModel.JobPosition = user.JobPosition.MapJobPositionToJobPositionModel();

            if (mapTitle)
                applicationUserModel.JobTitle = user.JobTitle.MapJobTitleToJobTitleModel();

            if (mapManager)
                applicationUserModel.Manager = user.Manager.MapApplicationUserToApplicationUserModel(false, false, false, false, false);

            if (mapReviewer)
                applicationUserModel.Reviewer = user.Reviewer.MapApplicationUserToApplicationUserModel(false, false, false, false, false);

            if (mapTemplate)
                applicationUserModel.Template = user.Template.MapTemplateToTemplateModel(mapSkills);

            return applicationUserModel;
        }

        public static ApplicationUser MapApplicationUserModelToApplicationUser(this ApplicationUserModel userModel, ApplicationUser user = null)
        {
            if (userModel == null)
                return null;

            if (user == null)
            {
                user = new ApplicationUser();
            }

            user.Id = userModel.Id;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.JobPositionId = userModel.JobPositionId;
            user.JobTitleId = userModel.JobTitleId;
            user.ManagerId = userModel.ManagerId;
            user.ReviewerId = userModel.ReviewerId;
            user.TemplateId = userModel.TemplateId;
            user.EmploymentDate = userModel.EmploymentDate;
            user.UserName = userModel.Username;

            return user;
        }

        public static PerformanceEvaluationModel MapApplicationUserModelToPerformanceEvaluationModel(this ApplicationUserModel userModel)
        {
            if (userModel == null)
                return null;

            return new PerformanceEvaluationModel
            {
                ConsultantId = userModel.Id,
                ConsultantFirstName = userModel.FirstName,
                ConsultantLastName = userModel.LastName,
                ReviewerId = userModel.ReviewerId,
                ReviewerFirstName = userModel.Reviewer.FirstName,
                ReviewerLastName = userModel.Reviewer.LastName,
                ConsultantJobTitleId = userModel.JobTitleId,
                ConsultantJobPositionId = userModel.JobPositionId,
                ConsultantJobTitle = userModel.JobTitle,
                ConsultantJobPosition = userModel.JobPosition,
                TemplateId = userModel.TemplateId,
                Template = userModel.Template
            };           
        }
    }
}
