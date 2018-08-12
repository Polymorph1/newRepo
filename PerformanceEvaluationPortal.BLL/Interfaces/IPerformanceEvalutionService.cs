using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;

namespace PerformanceEvaluationPortal.BLL.Interfaces
{
    public interface IPerformanceEvalutionService
    {
        PerformanceEvaluationModel GetPerformanceEvaluation(int id);

        ICollection<PerformanceEvaluationInfoModel> GetPerformanceEvaluationsForReviewer(string reviewerId);

        ICollection<PerformanceEvaluationHistoryModel> GetPerformanceEvaluationsHistoryForReviewer(string reviewerId);

        ICollection<PerformanceEvaluationInfoModel> GetLatePerformanceEvaluationsForManager(string managerId);

        ICollection<PerformanceEvaluationHistoryModel> GetPerformanceEvaluationsHistoryForManager(string managerId);

        ICollection<PerformanceEvaluationHistoryModel> GetMyPerformanceEvaluations(string userId, bool signed = false);

        PerformanceEvaluationModel CreatePerformanceEvaluation(string userId);

        PerformanceEvaluationModel SaveOrSubmitPerformanceEvaluation(PerformanceEvaluationModel performanceEvaluationModel, bool isSubmit = false);

        bool AcknowledgePerformanceEvaluation(PerformanceEvaluationModel performanceEvaluationModel);
    }
}
