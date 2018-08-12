using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class JobTitle
    {
        public int JobTitleId { get; set; }

        public string Name { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public ICollection<PerformanceEvaluation> PerformanceEvaluations { get; set; }

    }
}
