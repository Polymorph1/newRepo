using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DomainModel;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class PerformanceEvaluationMapper
    {
        public static PerformanceEvaluationModel MapPerformanceEvaluationToPerformanceEvaluationModel(this PerformanceEvaluation performanceEvaluation, bool mapConsultant, bool mapJobTitle = true, bool mapJobPosition = true, bool mapTemplate = true)
        {
            if (performanceEvaluation == null)
                return null;

            var performanceEvaluationModel = new PerformanceEvaluationModel
            {
                Id = performanceEvaluation.PerformanceEvaluationId,
                ConsultantId = performanceEvaluation.ConsultantId,
                ConsultantFirstName = performanceEvaluation.ConsultantFirstName,
                ConsultantLastName = performanceEvaluation.ConsultantLastName,
                ConsultantJobTitleId = performanceEvaluation.JobTitleId,
                ConsultantJobPositionId = performanceEvaluation.JobPositionId,
                ReviewerId = performanceEvaluation.ReviewerId,
                ReviewerFirstName = performanceEvaluation.ReviewerFirstName,
                ReviewerLastName = performanceEvaluation.ReviewerLastName,
                StartDate = performanceEvaluation.StartDate,
                EndDate = performanceEvaluation.EndDate,
                IsSubmited = performanceEvaluation.IsSubmitted,
                SubmitedDate = performanceEvaluation.SubmittedDate,
                ConsultantComment = performanceEvaluation.ConsultantComment,
                SignedOnDate = performanceEvaluation.SignedOnDateByConsultant,              
                TemplateId = performanceEvaluation.TemplateId
            };

            if (mapJobPosition)
            {
                performanceEvaluationModel.ConsultantJobPosition = performanceEvaluation.JobPosition.MapJobPositionToJobPositionModel();
            }

            if (mapJobTitle)
            {
                performanceEvaluationModel.ConsultantJobTitle = performanceEvaluation.JobTitle.MapJobTitleToJobTitleModel();
            }

            if (mapTemplate)
            {
                performanceEvaluationModel.Template = performanceEvaluation.Template.MapTemplateToTemplateModel(true);
            }

            if (performanceEvaluation.PerformanceEvaluationSkills != null)
            {
                performanceEvaluationModel.PerformanceEvaluationSkills = performanceEvaluation.PerformanceEvaluationSkills
                                .Select(x => x.MapPerformanceEvaluationSkillToPerformanceEvaluationSkillModel()).ToList();
            }

            if (mapConsultant)
            {
                performanceEvaluationModel.Consultant = performanceEvaluation.Consultant.MapApplicationUserToApplicationUserModel();
            }
            
            return performanceEvaluationModel;
        }

        public static PerformanceEvaluation MapPerformanceEvaluationModelToPerformanceEvaluation(this PerformanceEvaluationModel peModel)
        {
            if (peModel == null)
                return null;

            var performanceEvaluation = new PerformanceEvaluation
            {
                PerformanceEvaluationId = peModel.Id,
                Consultant = peModel.Consultant.MapApplicationUserModelToApplicationUser(),
                ConsultantId = peModel.ConsultantId,
                ConsultantFirstName = peModel.ConsultantFirstName,
                ConsultantLastName = peModel.ConsultantLastName,
                JobTitleId = peModel.ConsultantJobTitleId,
                JobPositionId = peModel.ConsultantJobPositionId,
                Reviewer = peModel.Reviewer.MapApplicationUserModelToApplicationUser(),
                ReviewerId = peModel.ReviewerId,
                ReviewerFirstName = peModel.ReviewerFirstName,
                ReviewerLastName = peModel.ReviewerLastName,
                StartDate = peModel.StartDate,
                EndDate = peModel.EndDate,
                IsSubmitted = peModel.IsSubmited,
                SubmittedDate = peModel.SubmitedDate,
                TemplateId = peModel.TemplateId,
                //PerformanceEvaluationSkills = peModel.PerformanceEvaluationSkills
                //                                        .Select(x => x.MapPerformanceEvaluationSkillModelToPerformanceEvaluationSkill())
                //                                        .ToList()
        };                                       
            return performanceEvaluation;
        }
    }
}
