using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DTO
{
    public class PerformanceEvaluationInfoModel
    {
        public int Id { get; set; }

        public string ReviewerId { get; set; }
        public ApplicationUserModel Reviewer { get; set; }

        public string ReviewerFirstName { get; set; }

        public string ReviewerLastName { get; set; }

        public string ConsultantId { get; set; }
        public ApplicationUserModel Consultant { get; set; }

        public string ConsultantFirstName { get; set; }

        public string ConsultantLastName { get; set; }

        public int ConsultantJobTitleId { get; set; }
        public JobTitleModel ConsultantJobTitle { get; set; }

        public int ConsultantJobPositionId { get; set; }
        public JobPositionModel ConsultantJobPosition { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsLate { get; set; }

        public bool IsNearDueDate { get; set; }

        public bool InEditStage { get; set; }

        public bool InCreateStage { get; set; }

        public int? TemplateId { get; set; }
        public TemplateModel Template { get; set; }
    }
}
