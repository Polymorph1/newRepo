using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class PerformanceEvaluation
    {
        public int PerformanceEvaluationId { get; set; }

        public string ConsultantId { get; set; }
        public ApplicationUser Consultant { get; set; }

        public string ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }

        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsSubmitted { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string ConsultantFirstName { get; set; }

        public string ConsultantLastName { get; set; }

        public string ReviewerFirstName { get; set; }

        public string ReviewerLastName { get; set; }

        public ICollection<PerformanceEvaluationSkill> PerformanceEvaluationSkills { get; set; }

        public string ConsultantComment { get; set; }

        public DateTime? SignedOnDateByConsultant { get; set; }

        public int? TemplateId { get; set; }
        public Template Template { get; set; }

    }
}
