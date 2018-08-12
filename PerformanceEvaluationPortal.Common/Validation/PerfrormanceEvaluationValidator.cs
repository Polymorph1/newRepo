using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;

namespace PerformanceEvaluationPortal.Common.Validation
{
    public static class PerfrormanceEvaluationValidator
    {
        public static bool IsInfoValid(this PerformanceEvaluationInfoModel peInfoModel, out List<string> errorMessages)
        {
            errorMessages = new List<string>();

            if (peInfoModel == null)
            {
                errorMessages.Add("Performance evaluation info model cannot be null");
                return false;
            }

            if (peInfoModel.ConsultantJobTitleId == 0)
            {
                errorMessages.Add("User must have a job title.");
            }

            if (peInfoModel.ConsultantJobPositionId == 0)
            {
                errorMessages.Add("User must have a job position.");
            }

            if (peInfoModel.StartDate == null)
            {
                errorMessages.Add("Performance evaluation model must have start date defined.");
            }

            if (peInfoModel.DueDate == null)
            {
                errorMessages.Add("Performance evaluation model must have end date defined.");
            }

            return errorMessages.Count == 0;
        }
        public static bool IsValid(this PerformanceEvaluationModel peModel, out List<string> errorMessages, bool submit = false)
        {
            errorMessages = new List<string>();


            if (peModel == null)
            {
                errorMessages.Add("PerformanceEvaluationModel cannot be null.");

                return false;
            }

            if (peModel.ConsultantJobTitleId == 0)
            {
                errorMessages.Add("User must have a job title.");
            }

            if (peModel.ConsultantJobPositionId == 0)
            {
                errorMessages.Add("User must have a job position.");
            }

            if (peModel.StartDate == null)
            {
                errorMessages.Add("Performance evaluation model must have start date defined.");
            }

            if (peModel.EndDate == null)
            {
                errorMessages.Add("Performance evaluation model must have end date defined.");
            }

            if (peModel.IsSubmited)
            {
                errorMessages.Add("Cannot edit submited performance evaluation.");
            }

            // Performance Evaluation Skills validation
            foreach (var peSkillModel in peModel.PerformanceEvaluationSkills)
            {
                if (peSkillModel.PerformanceEvaluationId == 0)
                {
                    errorMessages.Add("Performance evaluation skill model must be part of Performance evaluation");
                }

                if (peSkillModel.SkillId == 0)
                {
                    errorMessages.Add("Performance evaluation skill model must have skill id defined.");
                }

                List<string> correctGrades = new List<string>(new string[] { "E", "M", "N" });
                if (peSkillModel.Grade != null)
                {
                    if (!correctGrades.Any(item => peSkillModel.Grade.Contains(item)))
                    {
                        errorMessages.Add("Performance evaluation skill model grades must have values 'E', 'M' or 'N'.");
                    }
                }

                if (submit)
                {
                    if (String.IsNullOrWhiteSpace(peSkillModel.Comment))
                    {
                        errorMessages.Add("Performance evaluation skill model must have comment defined before submit.");
                    }

                    if (String.IsNullOrWhiteSpace(peSkillModel.Grade))
                    {
                        errorMessages.Add("Performance evaluation skill model must have grade defined before submit.");
                    }
                }
            }

            return errorMessages.Count == 0;
        }
    }
}
