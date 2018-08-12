using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.Common;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class TemplateMapper
    {
        public static TemplateModel MapTemplateToTemplateModel(this Template template, bool mapSkills)
        {
            if (template == null)
                return null;

            var templateModel = new TemplateModel
            {
                Id = template.TemplateId,
                Name = template.TemplateName,
                IsArchived = template.IsArchived,
                TemplateDurationId = template.TemplateDurationId,
                TemplateDuration = template.TemplateDuration.MapTemplateDurationToTemplateDurationModel()
                
            };
            

           
            if (mapSkills)
            {
                templateModel.Skills = template.Skills.Select(x => x.MapSkillToSkillModel()).ToList();
            }

            return templateModel;
        }
        public static Template MapTemplateModelToTemplate(this TemplateModel templateModel, Template template = null)
        {
            if (templateModel == null)
                return null;

            if (template == null)
                template = new Template();

            template.TemplateId = templateModel.Id;

            template.TemplateName = templateModel.Name;

            template.TemplateDurationId = templateModel.TemplateDurationId;

            template.IsArchived = templateModel.IsArchived;

            template.Skills = templateModel.Skills.Select(x => x.MapSkillModelToSkill()).ToList();

            return template;
        }
    }
}
