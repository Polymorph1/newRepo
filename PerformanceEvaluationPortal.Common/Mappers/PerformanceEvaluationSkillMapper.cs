using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class PerformanceEvaluationSkillMapper
    {
        public static PerformanceEvaluationSkillModel MapPerformanceEvaluationSkillToPerformanceEvaluationSkillModel(this PerformanceEvaluationSkill performanceEvaluationSkill)
        {
            if (performanceEvaluationSkill == null)
                return null;

            var performanceEvaluationSkillModel = new PerformanceEvaluationSkillModel
            {
                Id = performanceEvaluationSkill.PerformanceEvaluationSkillId,
                Grade = performanceEvaluationSkill.Grade,
                Comment = performanceEvaluationSkill.PESkillComment,
                Skill = performanceEvaluationSkill.Skill.MapSkillToSkillModel(),
                SkillId = performanceEvaluationSkill.SkillId,
                PerformanceEvaluationId = performanceEvaluationSkill.PerformanceEvaluationId
            };

            return performanceEvaluationSkillModel;
        }
        //TO DO:Change or Remove
        public static PerformanceEvaluationSkill MapPerformanceEvaluationSkillModelToPerformanceEvaluationSkill(this PerformanceEvaluationSkillModel performanceEvaluationSkillModel)
        {
            if (performanceEvaluationSkillModel == null)
                return null;

            var performanceEvaluationSkill = new PerformanceEvaluationSkill
            {
                Grade = performanceEvaluationSkillModel.Grade,
                PESkillComment = performanceEvaluationSkillModel.Comment,
                SkillId = performanceEvaluationSkillModel.SkillId,
                Skill = performanceEvaluationSkillModel.Skill.MapSkillModelToSkill(),
                PerformanceEvaluationId = performanceEvaluationSkillModel.PerformanceEvaluationId,
                PerformanceEvaluation = performanceEvaluationSkillModel.PerformanceEvaluation.MapPerformanceEvaluationModelToPerformanceEvaluation()
            };

            return performanceEvaluationSkill;
        }

        public static PerformanceEvaluationSkillModel MapSkillModelToPerformanceEvaluationSkillModel(this SkillModel skillModel, PerformanceEvaluation pe)
        {
            if (skillModel == null)
                return null;

            var performanceEvaluationSkillModel = new PerformanceEvaluationSkillModel
            {
                PerformanceEvaluationId = pe.PerformanceEvaluationId,
                Skill = skillModel,
                SkillId = skillModel.Id,
            };

            return performanceEvaluationSkillModel;
        }
    }
}
