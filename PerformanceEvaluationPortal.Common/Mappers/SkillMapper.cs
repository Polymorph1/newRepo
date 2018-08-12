using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class SkillMapper
    {
        public static SkillModel MapSkillToSkillModel(this Skill skill)
        {
            if (skill == null)
                return null;

            var skillModel = new SkillModel
            {
                Id = skill.SkillId,
                Description = skill.SkillDescription,
                Name = skill.SkillName
            };

            return skillModel;
        }
        //TO DO:Change or Remove
        public static Skill MapSkillModelToSkill(this SkillModel skillModel)
        {
            if (skillModel == null)
                return null;

            return new Skill
            {
                SkillDescription = skillModel.Description,
                SkillId = skillModel.Id,
                SkillName = skillModel.Name
            };
        }

        public static PerformanceEvaluationSkillModel MapSkillToPerformanceEvaluationSkillModel(this Skill skill, int performanceEvaluationId)
        {
            if (skill == null)
                return null;

            return new PerformanceEvaluationSkillModel
            {
                PerformanceEvaluationId = performanceEvaluationId,
                Comment = null,
                Grade = null,
                Skill = skill.MapSkillToSkillModel(),
                SkillId = skill.SkillId
            };
        }

        public static PerformanceEvaluationSkill MapSkillToPerformanceEvaluationSkill(this Skill skill, int performanceEvaluationId)
        {
            if (skill == null)
                return null;

            return new PerformanceEvaluationSkill
            {
                PerformanceEvaluationId = performanceEvaluationId,
                PESkillComment = null,
                Grade = null,
                Skill = skill,
                SkillId = skill.SkillId
            };
        }
    }
}
