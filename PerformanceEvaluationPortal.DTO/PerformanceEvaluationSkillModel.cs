using System.Collections;
using System.Collections.Generic;

namespace PerformanceEvaluationPortal.DTO
{
    public class PerformanceEvaluationSkillModel
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Grade { get; set; }

        public int SkillId { get; set; }
        public SkillModel Skill { get; set; }

        public int PerformanceEvaluationId { get; set; }
        public PerformanceEvaluationModel PerformanceEvaluation { get; set; }
    }
}