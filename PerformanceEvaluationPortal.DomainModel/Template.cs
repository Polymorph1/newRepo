using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class Template
    {
        public int TemplateId { get; set; }

        public string TemplateName { get; set; }

        public bool IsArchived { get; set; }

        public int TemplateDurationId { get; set; }
        public TemplateDuration TemplateDuration { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<ApplicationUser> ApplicationsUsers { get; set; }
        public ICollection<PerformanceEvaluation> PerformanceEvaluations { get; set; }



    }
}
