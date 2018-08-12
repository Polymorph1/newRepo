using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DomainModel;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class PerformanceEvaluationHistoryInfoMapper
    {
        public static PerformanceEvaluationHistoryModel MapPerformanceEvaluationToPerformanceEvaluationHistoryInfoModel(this PerformanceEvaluation performanceEvaluation)
        {
            if (performanceEvaluation == null)
                return null;

            var performanceEvaluationHistoryInfoModel = new PerformanceEvaluationHistoryModel
            {
                Id = performanceEvaluation.PerformanceEvaluationId,
                ConsultantFirstName = performanceEvaluation.ConsultantFirstName,
                ConsultantLastName = performanceEvaluation.ConsultantLastName,
                ConsultantJobTitle = performanceEvaluation.JobTitle.Name,
                ConsultantJobPosition = performanceEvaluation.JobPosition.PositionName,
                ReviewerFirstName = performanceEvaluation.ReviewerFirstName,
                ReviewerLastName = performanceEvaluation.ReviewerLastName,
                DateSubmited = performanceEvaluation.SubmittedDate
            };

            return performanceEvaluationHistoryInfoModel;
        }
    }
}
