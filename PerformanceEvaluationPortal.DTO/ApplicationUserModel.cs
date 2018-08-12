using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DTO
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int JobTitleId { get; set; }
        public JobTitleModel JobTitle { get; set; }

        public int JobPositionId { get; set; }
        public JobPositionModel JobPosition { get; set; }

        public string ManagerId { get; set; }
        public ApplicationUserModel Manager { get; set; }

        public bool IsManager { get; set; }

        public string ReviewerId { get; set; }
        public ApplicationUserModel Reviewer { get; set; }

        public bool IsReviewer { get; set; }

        public int? TemplateId { get; set; }
        public TemplateModel Template { get; set; }

        public DateTime EmploymentDate { get; set; }

        public string Username { get; set; }


    }
}
