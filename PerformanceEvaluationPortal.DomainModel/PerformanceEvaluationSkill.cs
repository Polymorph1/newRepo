using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class PerformanceEvaluationSkill
    {
        public int PerformanceEvaluationSkillId { get; set; }

        public string PESkillComment { get; set; }

        public string Grade { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public int PerformanceEvaluationId { get; set; }
        public PerformanceEvaluation PerformanceEvaluation { get; set; }


    }
}
