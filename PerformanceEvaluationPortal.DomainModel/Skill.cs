using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class Skill
    {
        public int SkillId { get; set; }

        public string SkillName { get; set; }

        public string SkillDescription { get; set; }

        public ICollection<Template> Templates { get; set; }

        public ICollection<PerformanceEvaluationSkill> PerformanceEvaluationSkills { get; set; }
    }
}
