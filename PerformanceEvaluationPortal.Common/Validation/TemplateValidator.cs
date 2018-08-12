using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Validation
{
    public static class TemplateValidator
    {
        public static bool IsValid(this TemplateModel templateModel, out List<string> errorMessages)
        {
            errorMessages = new List<string>();

            if (templateModel == null)
            {
                errorMessages.Add("Template model cannot be null.");

                return false;
            }

            if (String.IsNullOrWhiteSpace(templateModel.Name))
            {
                errorMessages.Add("Template must have name.");
            }

            if (templateModel.Skills == null || templateModel.Skills.Count <= 0)
            {
                errorMessages.Add("Template must have at least one skill.");
            }

            if (templateModel.TemplateDurationId <= 0)
            {
                errorMessages.Add("Template must have template duration.");

            }

            return errorMessages.Count == 0;
        }
    }
}
