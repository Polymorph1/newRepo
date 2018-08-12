using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DomainModel;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class PerformanceEvaluationInfoMapper
    {
        public static PerformanceEvaluationInfoModel MapApplicationUserToPerformanceEvaluationInfoModel(this ApplicationUser user, DateTime startDate, DateTime endDate)
        {
            if (user == null)
                return null;

            var performanceEvaluationInfoModel = new PerformanceEvaluationInfoModel
            {
                ConsultantId = user.Id,
                ConsultantFirstName = user.FirstName,
                ConsultantLastName = user.LastName,
                ConsultantJobTitleId = user.JobTitleId,
                ConsultantJobTitle = user.JobTitle.MapJobTitleToJobTitleModel(),
                ConsultantJobPositionId = user.JobPositionId,
                ConsultantJobPosition = user.JobPosition.MapJobPositionToJobPositionModel(),
                ReviewerId = user.ReviewerId,
                ReviewerFirstName = user.Reviewer.FirstName,
                ReviewerLastName = user.Reviewer.LastName,
                TemplateId = user.TemplateId,
                Template = user.Template.MapTemplateToTemplateModel(true),
                StartDate = startDate,
                DueDate = endDate,
            };

            return performanceEvaluationInfoModel;
        }

        public static PerformanceEvaluationInfoModel MapPerformanceEvaluationToPerformanceEvaluationInfoModel(this PerformanceEvaluation performanceEvaluation)
        {
            var performanceEvaluationInfoModel = new PerformanceEvaluationInfoModel
            {
                Id = performanceEvaluation.PerformanceEvaluationId,
                ConsultantId = performanceEvaluation.ConsultantId,
                ConsultantFirstName = performanceEvaluation.Consultant.FirstName,
                ConsultantLastName = performanceEvaluation.Consultant.LastName,
                ConsultantJobTitleId = performanceEvaluation.Consultant.JobTitleId,
                ConsultantJobTitle = performanceEvaluation.Consultant.JobTitle.MapJobTitleToJobTitleModel(),
                ConsultantJobPositionId = performanceEvaluation.Consultant.JobPositionId,
                ConsultantJobPosition = performanceEvaluation.Consultant.JobPosition.MapJobPositionToJobPositionModel(),
                ReviewerId = performanceEvaluation.ReviewerId,
                ReviewerFirstName = performanceEvaluation.Reviewer.FirstName,
                ReviewerLastName = performanceEvaluation.Reviewer.LastName,
                TemplateId = performanceEvaluation.TemplateId,
                Template = performanceEvaluation.Template.MapTemplateToTemplateModel(true),
                StartDate = performanceEvaluation.StartDate,
                DueDate = performanceEvaluation.EndDate
            };

            return performanceEvaluationInfoModel;
        }

        public static PerformanceEvaluation MapPerformanceEvaluationInfoModelToPerformanceEvaluation(this PerformanceEvaluationInfoModel peInfoModel, bool mapTemplate = true, bool mapJobTitle = true, bool mapJobPosition = true)
        {
            if (peInfoModel == null)
                return null;

            PerformanceEvaluation pe = new PerformanceEvaluation
            {
                PerformanceEvaluationId = peInfoModel.Id,

                ConsultantId = peInfoModel.ConsultantId,
                ConsultantFirstName = peInfoModel.ConsultantFirstName,
                ConsultantLastName = peInfoModel.ConsultantLastName,
                JobTitleId = peInfoModel.ConsultantJobTitleId,
                JobPositionId = peInfoModel.ConsultantJobPositionId,
                ReviewerId = peInfoModel.ReviewerId,
                ReviewerFirstName = peInfoModel.ReviewerFirstName,
                ReviewerLastName = peInfoModel.ReviewerLastName,
                StartDate = peInfoModel.StartDate,
                EndDate = peInfoModel.DueDate,
                TemplateId = peInfoModel.TemplateId
            };

            if (mapTemplate)
            {
                pe.Template = peInfoModel.Template.MapTemplateModelToTemplate();
            }

            if (mapJobTitle)
            {
                pe.JobTitle = peInfoModel.ConsultantJobTitle.MapJobTitleModelToJobTitle();
            }

            if (mapJobPosition)
            {
                pe.JobPosition = peInfoModel.ConsultantJobPosition.MapJobPositionModelToJobPosition();
            }

            return pe;
        }
    }
}
