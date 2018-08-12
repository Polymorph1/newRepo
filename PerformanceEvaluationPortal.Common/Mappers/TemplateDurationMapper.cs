using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common
{
    public static class TemplateDurationMapper
    {
        public static TemplateDurationModel MapTemplateDurationToTemplateDurationModel(this TemplateDuration templateDuration)
        {
            if (templateDuration == null)
                return null;

            var templateDurationModel = new TemplateDurationModel
            {
                Id = templateDuration.TemplateDurationId,
                Duration = templateDuration.Duration
            };

            return templateDurationModel;
        }
        //TO DO:Change or Remove
        public static TemplateDuration MapTemplateDurationModelToTemplateDuration(this TemplateDurationModel templateDurationModel)
        {
            if (templateDurationModel == null)
                return null;

            var templateDuration = new TemplateDuration
            {
                TemplateDurationId = templateDurationModel.Id,
                Duration = templateDurationModel.Duration
            };

            return templateDuration;
        }
    }
}
