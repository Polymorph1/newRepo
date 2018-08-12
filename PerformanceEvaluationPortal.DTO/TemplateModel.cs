using PerformanceEvaluationPortal.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DTO
{
    public class TemplateModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public int TemplateDurationId { get; set; }
        public TemplateDurationModel TemplateDuration { get; set; }

        public int SkillId { get; set; }
        public ICollection<SkillModel> Skills { get; set; }

        public bool canBeArchived { get; set; }

        public bool  canBeEdited { get; set; }
        

    }
}
