using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DomainModel
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime EmploymentDate { get; set; }

        public string ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }
        public ICollection<ApplicationUser> UsersIWriteReviewFor { get; set; }

        public string ManagerId { get; set; }
        public ApplicationUser Manager { get; set; }
        public ICollection<ApplicationUser> UsersIManage { get; set; }

        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }

        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }

        public int? TemplateId { get; set; }
        public Template Template { get; set; }

        public ICollection<PerformanceEvaluation> ReviewerPerformanceEvaluations { get; set; }

        public ICollection<PerformanceEvaluation> ConsultantPerformanceEvaluations { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }


    }
}
