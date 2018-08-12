namespace PerformanceEvaluationPortal.DAL.Migrations
{
    using DomainModel;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PerformanceEvaluationPortal.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PerformanceEvaluationPortal.DAL.ApplicationDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
            //try
            //{

            base.Seed(context);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            #region *** Add Roles ***

            const string adminRole = "Admin";
            if (!context.Roles.Any(r => r.Name == adminRole))
                roleManager.Create(new IdentityRole { Name = adminRole });

            

            #endregion

            #region *** Add Job Titles ***

            var jobTitleSoftwareDeveloper = CreateOrRetrieveJobTitle(context, "Software Developer");
            var jobTitleDatabaseDeveloper = CreateOrRetrieveJobTitle(context, "Database Developer");
            var jobTitleQualityAssurance = CreateOrRetrieveJobTitle(context, "Quality Assurance");

            #endregion

            #region *** Add Job Positions ***

            var jobPositionJuniorDeveloperOne = CreateOrRetrieveJobPosition(context, "Junior Developer 1");
            var jobPositionJuniorDeveloperTwo = CreateOrRetrieveJobPosition(context, "Junior Developer 2");
            var jobPositionJuniorDeveloperThree = CreateOrRetrieveJobPosition(context, "Junior Developer 3");
            var jobPositionSeniorDeveloperOne = CreateOrRetrieveJobPosition(context, "Senior Developer 1");
            var jobPositionSeniorDeveloperTwo = CreateOrRetrieveJobPosition(context, "Senior Developer 2");
            var jobPositionSeniorDeveloperThree = CreateOrRetrieveJobPosition(context, "Senior Developer 3");

            #endregion

            #region *** Add Template Durations ***

            var templateDurationOneYear = CreateOrRetrieveTemplateDuration(context, 12);
            var templateDurationSixMonths = CreateOrRetrieveTemplateDuration(context, 6);
            var templateDurationThreeMonths = CreateOrRetrieveTemplateDuration(context, 3);

            #endregion

            #region *** Add Skills ***

            var skillCareer = CreateOrRetrieveSkill(context, "Career skill", "Description of career skill");
            var skillCommunication = CreateOrRetrieveSkill(context, "Communication skill", "Description of communication skill");
            var skillAnalyticalThinking = CreateOrRetrieveSkill(context, "Analytical thinking skill", "Description of analytical thinking skill");

            #endregion

            #region *** Add Templates ***
            var templateJuniorDeveloperThreeMonths = CreateOrRetrieveTemplate(context, "Junior Developer 1", false);
            templateJuniorDeveloperThreeMonths.TemplateDuration = templateDurationThreeMonths;
            if (templateJuniorDeveloperThreeMonths.Skills == null || templateJuniorDeveloperThreeMonths.Skills.Count == 0)
            {
                templateJuniorDeveloperThreeMonths.Skills = new List<Skill>();
                templateJuniorDeveloperThreeMonths.Skills.Add(skillCareer);
                templateJuniorDeveloperThreeMonths.Skills.Add(skillCommunication);
            }

            var templateJuniorDeveloperSixMonths = CreateOrRetrieveTemplate(context, "Junior Developer 2", false);
            templateJuniorDeveloperSixMonths.TemplateDuration = templateDurationSixMonths;
            if (templateJuniorDeveloperSixMonths.Skills == null || templateJuniorDeveloperSixMonths.Skills.Count == 0)
            {
                templateJuniorDeveloperSixMonths.Skills = new List<Skill>();
                templateJuniorDeveloperSixMonths.Skills.Add(skillCareer);
                templateJuniorDeveloperSixMonths.Skills.Add(skillCommunication);
            }

            var templateSeniorDeveloperSixMonths = CreateOrRetrieveTemplate(context, "Senior Developer 1", false);
            templateSeniorDeveloperSixMonths.TemplateDuration = templateDurationSixMonths;
            if (templateSeniorDeveloperSixMonths.Skills == null || templateSeniorDeveloperSixMonths.Skills.Count == 0)
            {
                templateSeniorDeveloperSixMonths.Skills = new List<Skill>();
                templateSeniorDeveloperSixMonths.Skills.Add(skillCareer);
                templateSeniorDeveloperSixMonths.Skills.Add(skillCommunication);
                templateSeniorDeveloperSixMonths.Skills.Add(skillAnalyticalThinking);
            }

            var templateSeniorDeveloperOneYear = CreateOrRetrieveTemplate(context, "Senior Developer 2", false);
            templateSeniorDeveloperOneYear.TemplateDuration = templateDurationOneYear;
            if (templateSeniorDeveloperOneYear.Skills == null || templateSeniorDeveloperOneYear.Skills.Count == 0)
            {
                templateSeniorDeveloperOneYear.Skills = new List<Skill>();
                templateSeniorDeveloperOneYear.Skills.Add(skillCareer);
                templateSeniorDeveloperOneYear.Skills.Add(skillCommunication);
                templateSeniorDeveloperOneYear.Skills.Add(skillAnalyticalThinking);
            }

            #endregion          

            #region *** Add Users ***

            var admin = CreateOrRetrieveUser(context, userManager, "admin", "Edin", "Besic", "password",
            jobTitleDatabaseDeveloper, jobPositionSeniorDeveloperTwo, null, DateTime.Now.AddYears(-2));
            userManager.AddToRole(admin.Id, adminRole);

            var manager = CreateOrRetrieveUser(context, userManager, "manager", "Adna", "Durakovic", "password",
                jobTitleSoftwareDeveloper, jobPositionSeniorDeveloperThree, templateSeniorDeveloperOneYear, DateTime.Now.AddYears(-1));
            
           

            var consultantsManager = CreateOrRetrieveUser(context, userManager, "cmanager", "Mehrudin", "Herceg", "password",
                jobTitleDatabaseDeveloper, jobPositionSeniorDeveloperTwo, templateSeniorDeveloperOneYear, DateTime.Now.AddMonths(-10)); 
                       
            consultantsManager.Reviewer = manager;
            consultantsManager.Manager = manager;
           
            var consultantsReviewer = CreateOrRetrieveUser(context, userManager, "creviewer", "Almedin", "Velija", "password",
                jobTitleSoftwareDeveloper, jobPositionSeniorDeveloperTwo, templateSeniorDeveloperOneYear, DateTime.Now.AddMonths(-9));  
                      
            consultantsReviewer.Manager = manager;
            consultantsReviewer.Reviewer = manager;

            var consultantSeniorOne = CreateOrRetrieveUser(context, userManager, "consultant1", "Zoe", "Sugg", "password",
                jobTitleSoftwareDeveloper, jobPositionSeniorDeveloperOne, templateSeniorDeveloperSixMonths, DateTime.Now.AddMonths(-5).AddDays(-25));
            consultantSeniorOne.Manager = manager;
            consultantSeniorOne.Reviewer = consultantsReviewer;

            var consultantJuniorTwo = CreateOrRetrieveUser(context, userManager, "consultant", "Elmana", "Pelko", "password",
                jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperTwo, templateJuniorDeveloperSixMonths, DateTime.Now.AddYears(-6));
            consultantJuniorTwo.Manager = consultantsManager;
            consultantJuniorTwo.Reviewer = consultantsReviewer;
            
            var consultantJuniorOne = CreateOrRetrieveUser(context, userManager, "consultant2", "Mujo", "Mujic", "password",
                jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperOne, templateJuniorDeveloperThreeMonths, DateTime.Now.AddMonths(-6));
            consultantJuniorOne.Manager = consultantsManager;
            consultantJuniorOne.Reviewer = consultantsReviewer;

            var managerJuniorOne = CreateOrRetrieveUser(context, userManager, "manager1", "Dino", "Sporer", "password",
               jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperOne, templateJuniorDeveloperThreeMonths, DateTime.Now.AddMonths(-6));
            consultantJuniorOne.Manager = consultantsManager;
            consultantJuniorOne.Reviewer = consultantsReviewer;

            var reviewerJuniorOne = CreateOrRetrieveUser(context, userManager, "reviewer1", "Selma", "Muminovic", "password",
               jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperOne, templateJuniorDeveloperThreeMonths, DateTime.Now.AddMonths(-3).AddDays(9));
            consultantJuniorOne.Manager = consultantsManager;
            consultantJuniorOne.Reviewer = consultantsReviewer;

            var managerSeniorOne = CreateOrRetrieveUser(context, userManager, "manager2", "Amina", "Aljicevic", "password",
               jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperOne, templateJuniorDeveloperThreeMonths, DateTime.Now.AddMonths(-6).AddDays(9));
            consultantJuniorOne.Manager = consultantsManager;
            consultantJuniorOne.Reviewer = consultantsReviewer;

            var reviewerSeniorOne = CreateOrRetrieveUser(context, userManager, "reviewer2", "Semir", "Alagic", "password",
               jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperOne, templateJuniorDeveloperThreeMonths, DateTime.Now.AddMonths(-12));
            consultantJuniorOne.Manager = consultantsManager;
            consultantJuniorOne.Reviewer = consultantsReviewer;

            var consultantDeveloperOne = CreateOrRetrieveUser(context, userManager, "consultant3", "Amel", "Sazic", "password",
               jobTitleSoftwareDeveloper, jobPositionJuniorDeveloperOne, templateJuniorDeveloperThreeMonths, DateTime.Now.AddMonths(-12).AddDays(-9));
            consultantJuniorOne.Manager = consultantsManager;
            consultantJuniorOne.Reviewer = consultantsReviewer;

            #endregion

            #region *** Add PEs ***

            var peNotSubmitted = CreateOrRetrievePerformanceEvaluation(context, consultantJuniorTwo, consultantsReviewer, DateTime.Now.AddMonths(-6), DateTime.Now, false,consultantJuniorTwo.Template);

            var peSubmitted = CreateOrRetrievePerformanceEvaluation(context, consultantJuniorOne, consultantsReviewer, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-3), true, consultantJuniorOne.Template);
            peSubmitted.SubmittedDate = DateTime.Now.AddDays(-100);
            peSubmitted.SignedOnDateByConsultant = DateTime.Now.AddDays(-95);
            peSubmitted.ConsultantComment = "Great PE!";

            #endregion

            #region *** Add PE Skills ***

            var peSkillCareer = CreateOrRetrievePerformanceEvaluationSkill(context, peNotSubmitted, skillCareer, "M");
            var peSkillCommunication = CreateOrRetrievePerformanceEvaluationSkill(context, peNotSubmitted, skillCommunication, "E");
            var peSkillAnalyticalThinking = CreateOrRetrievePerformanceEvaluationSkill(context, peNotSubmitted, skillAnalyticalThinking, "M");

            var peSubmittedSkillCareer = CreateOrRetrievePerformanceEvaluationSkill(context, peSubmitted, skillCareer, "M", "Employee must work on...");
            var peSubmittedSkillCommunication = CreateOrRetrievePerformanceEvaluationSkill(context, peSubmitted, skillCommunication, "E", "Excelent job!");

            #endregion

            context.SaveChanges();

            //Uncomment if there is need to debug seed method
            //            context.SaveChanges();
            //        }
            //            catch (DbEntityValidationException e)
            //            {
            //                foreach (var eve in e.EntityValidationErrors)
            //                {
            //                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //                    foreach (var ve in eve.ValidationErrors)
            //                    {
            //                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                            ve.PropertyName, ve.ErrorMessage);
            //                    }
            //}
            //                throw;
            //            }

        }
        private ApplicationUser CreateOrRetrieveUser(ApplicationDbContext context, UserManager<ApplicationUser> userManager, string username, string firstName, string lastName, string password, JobTitle jobTitle, JobPosition jobPosition, Template template,
            DateTime dateOfEmployment)
        {
            var user = context.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = username,
                    Template = template,
                    JobPosition = jobPosition,
                    JobTitle = jobTitle,
                    EmploymentDate=dateOfEmployment
                };
                userManager.Create(user, password);
            }
            return user;
        }

        private JobTitle CreateOrRetrieveJobTitle(ApplicationDbContext context, string title)
        {
            var jobTitle = context.JobTitles.Where(x => x.Name == title).FirstOrDefault();

            if (jobTitle == null)
            {
                jobTitle = new JobTitle { Name = title };
                context.JobTitles.Add(jobTitle);
            }

            return jobTitle;
        }


        private JobPosition CreateOrRetrieveJobPosition(ApplicationDbContext context, string positionName)
        {
            var jobPosition = context.JobPositions.Where(x => x.PositionName == positionName).FirstOrDefault();

            if (jobPosition == null)
            {
                jobPosition = new JobPosition { PositionName = positionName };
                context.JobPositions.Add(jobPosition);
            }

            return jobPosition;
        }

        private Template CreateOrRetrieveTemplate(ApplicationDbContext context, string templateName, bool isArchived)
        {
            var template = context.Templates.Where(x => x.TemplateName == templateName).FirstOrDefault();

            if (template == null)
            {
                template = new Template { TemplateName = templateName, IsArchived = isArchived };
                context.Templates.Add(template);
            }

            return template;
        }
        private TemplateDuration CreateOrRetrieveTemplateDuration(ApplicationDbContext context, int duration)
        {
            var templateDuration = context.TemplateDurations.Where(x => x.Duration == duration).FirstOrDefault();

            if (templateDuration == null)
            {
                templateDuration = new TemplateDuration { Duration =duration };
                context.TemplateDurations.Add(templateDuration);
            }

            return templateDuration;
        }

        private Skill CreateOrRetrieveSkill(ApplicationDbContext context, string skillName, string skillDescription)
        {
            var skill = context.Skills.Where(x => x.SkillName == skillName).FirstOrDefault();

            if (skill == null)
            {
                skill = new Skill { SkillName = skillName, SkillDescription = skillDescription };
                context.Skills.Add(skill);
            }

            return skill;
        }

        private PerformanceEvaluation CreateOrRetrievePerformanceEvaluation(ApplicationDbContext context, ApplicationUser consultant, ApplicationUser reviewer, DateTime startDate, DateTime endDate, bool isSubmited,Template template)
        {
            var performaceEvaluation = context.PerformanceEvaluations.Where(x => x.ConsultantId == consultant.Id).FirstOrDefault();

            if (performaceEvaluation == null)
            {
                performaceEvaluation = new PerformanceEvaluation
                {
                    Consultant = consultant,
                    Reviewer = reviewer,
                    JobTitle = consultant.JobTitle,
                    JobPosition = consultant.JobPosition,
                    StartDate = startDate,
                    EndDate = endDate,
                    IsSubmitted = isSubmited,
                    ConsultantFirstName = consultant.FirstName,
                    ConsultantLastName = consultant.LastName,
                    ReviewerFirstName = reviewer.FirstName,
                    ReviewerLastName = reviewer.LastName,
                    TemplateId = template.TemplateId
                    
                };
                context.PerformanceEvaluations.Add(performaceEvaluation);
            }

            return performaceEvaluation;
        }

        private PerformanceEvaluationSkill CreateOrRetrievePerformanceEvaluationSkill(ApplicationDbContext context, PerformanceEvaluation pe, Skill skill, string grade, string comment = null)
        {
            var performanceEvaluationSkill = context.PerformanceEvaluationsSkills.Where(x => x.PerformanceEvaluationId == pe.PerformanceEvaluationId).FirstOrDefault();

            if (performanceEvaluationSkill == null)
            {
                performanceEvaluationSkill = new PerformanceEvaluationSkill
                {
                    PerformanceEvaluation = pe,
                    Skill = skill,
                    Grade = grade,
                    PESkillComment = comment
                };
                context.PerformanceEvaluationsSkills.Add(performanceEvaluationSkill);
            }

            return performanceEvaluationSkill;
        }
    }
}
