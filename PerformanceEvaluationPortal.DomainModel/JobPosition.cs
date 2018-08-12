using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class JobPosition
    {
        public int JobPositionId { get; set; }

        public string PositionName { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public ICollection<PerformanceEvaluation> PerformanceEvaluations { get; set; }

    }
}
