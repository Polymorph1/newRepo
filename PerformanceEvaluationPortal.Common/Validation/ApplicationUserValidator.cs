using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Validation
{
    public static class ApplicationUserValidator
    {

        public static bool IsValid(this ApplicationUserModel userModel, out List<string> errorMessages)
        {
            errorMessages = new List<string>();

            Regex r = new Regex("^[a-zšđčćž A-ZŠĐČĆŽ]+$");
            //"^[a - zšđčćž A - ZŠĐČĆŽ] +$/gi"

            if (userModel == null)
            {
                errorMessages.Add("User model cannot be null.");

                return false;
            }


            if (!String.IsNullOrWhiteSpace(userModel.FirstName))
            {

                if (!r.IsMatch(userModel.FirstName))
                {
                    errorMessages.Add("User can only have letters in  firstname");
                }
            }

            if(!String.IsNullOrWhiteSpace(userModel.LastName))
            {
                if(!r.IsMatch(userModel.LastName))
                {
                    errorMessages.Add("Users can only have letters in lastname");
                }
            }

            if (String.IsNullOrWhiteSpace(userModel.FirstName))
            {
                errorMessages.Add("User must have first name.");
            }

            if (String.IsNullOrWhiteSpace(userModel.LastName))
            {
                errorMessages.Add("User must have last name.");
            }

            if (userModel.JobTitleId <= 0)
            {
                errorMessages.Add("User must have title id.");

            }

            if (userModel.JobPositionId <= 0)
            {
                errorMessages.Add("User must have positon id.");
            }

            if (userModel.ManagerId != null)
            {

                if (userModel.ReviewerId == null)
                {
                    errorMessages.Add("User must have reviewer id.");
                }

                if (userModel.TemplateId == null)
                {
                    errorMessages.Add("User must have template id.");
                }
            }
            if (userModel.EmploymentDate.Equals(DateTime.MinValue))
            {
                errorMessages.Add("User must have Employment date");
            }

            return errorMessages.Count == 0;
        }
    }
}
