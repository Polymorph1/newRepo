using PerformanceEvaluationPortal.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DTO
{
    public class PerformanceEvaluationHistoryModel
    {
        public int Id { get; set; }

        public string ReviewerFirstName { get; set; }

        public string ReviewerLastName { get; set; }

        public string ConsultantFirstName { get; set; }

        public string ConsultantLastName { get; set; }

        public string ConsultantJobTitle { get; set; }

        public string ConsultantJobPosition { get; set; }

        public DateTime? DateSubmited { get; set; }
    }
}
