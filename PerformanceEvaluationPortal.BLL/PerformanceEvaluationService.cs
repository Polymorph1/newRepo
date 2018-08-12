using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DAL;
using System.Data.Entity;
using PerformanceEvaluationPortal.Common.Mappers;
using PerformanceEvaluationPortal.Common.Validation;
using PerformanceEvaluationPortal.BLL;

namespace PerformanceEvaluationPortal.BLL
{
    public class PerformanceEvaluationService : IPerformanceEvalutionService
    {
        private ApplicationDbContext _context;
        public PerformanceEvaluationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PerformanceEvaluationModel GetPerformanceEvaluation(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", "Id must be a positive number.");

            var performanceEvaluation = _context.PerformanceEvaluations.Include(x => x.JobTitle)
                                                                    .Include(x => x.JobPosition)
                                                                    .Include(x => x.Consultant)
                                                                    .Include(x => x.Reviewer)
                                                                    .Include(x => x.Template)
                                                                    .Include("Template.Skills")
                                                                    .Include(x => x.PerformanceEvaluationSkills)
                                                                    .Include("PerformanceEvaluationSkills.Skill")
                                                                    .Where(x => x.PerformanceEvaluationId == id)
                                                                    .FirstOrDefault();

            if (performanceEvaluation == null)
                throw new ApplicationException(String.Format("Performance evaluation with id {0} does not exist", id));

            if (performanceEvaluation.PerformanceEvaluationSkills.Count == 0)
            {
                foreach (var skill in performanceEvaluation.Template.Skills)
                {
                    performanceEvaluation.PerformanceEvaluationSkills.Add(skill
                                .MapSkillToPerformanceEvaluationSkill(performanceEvaluation.PerformanceEvaluationId));
                }
            }
            else
            {
                foreach (var skill in performanceEvaluation.Template.Skills)
                {
                    if (performanceEvaluation.PerformanceEvaluationSkills.Where(x => x.Skill.SkillId == skill.SkillId).FirstOrDefault() == null)
                    {
                        performanceEvaluation.PerformanceEvaluationSkills.Add(skill
                                                       .MapSkillToPerformanceEvaluationSkill(performanceEvaluation.PerformanceEvaluationId));
                    }
                }
            }

            performanceEvaluation.PerformanceEvaluationSkills = performanceEvaluation.PerformanceEvaluationSkills
                                                                                    .OrderBy(x => x.SkillId)
                                                                                    .ToList();

            return performanceEvaluation.MapPerformanceEvaluationToPerformanceEvaluationModel(false);
        }
      
        public ICollection<PerformanceEvaluationHistoryModel> GetMyPerformanceEvaluations(string userId, bool signed = false)
        {
            if (String.IsNullOrWhiteSpace(userId))
                throw new ArgumentOutOfRangeException("userId", "User's id can't be null.");

            return _context.PerformanceEvaluations.Include(x => x.JobTitle)
                                                .Include(x => x.JobPosition)
                                                .Include(x => x.Consultant)
                                                .Include(x => x.Reviewer)
                                                .Include(x => x.PerformanceEvaluationSkills)
                                                .Where(x => x.ConsultantId == userId)
                                                .Where(x => x.IsSubmitted)
                                                .Where(x => signed ? x.SignedOnDateByConsultant != null : x.SignedOnDateByConsultant == null)
                                                .ToList()
                                                .OrderByDescending(x => x.SubmittedDate.Value.Date)
                                                .ThenBy(x => x.SubmittedDate.Value.TimeOfDay)
                                                .Select(x => x.MapPerformanceEvaluationToPerformanceEvaluationHistoryInfoModel())
                                                .ToList();
        }

        public ICollection<PerformanceEvaluationInfoModel> GetLatePerformanceEvaluationsForManager(string managerId)
        {
            if (String.IsNullOrWhiteSpace(managerId))
                throw new ArgumentOutOfRangeException("managerId", "Manager's id can't be null.");

            var reviewersIManage = _context.Users.Include(x => x.JobTitle)
                                            .Include(x => x.JobPosition)
                                            .Where(x => x.ManagerId == managerId)
                                            .Where(x => x.UsersIWriteReviewFor.Count > 0)
                                            .ToList();

            List<PerformanceEvaluationInfoModel> performanceEvaluationsForManager = new List<PerformanceEvaluationInfoModel>();

            foreach (var reviewer in reviewersIManage)
            {
                var pe = GetPerformanceEvaluationsForReviewer(reviewer.Id).Where(x => x.IsLate);
                performanceEvaluationsForManager.AddRange(pe);
            }

            return performanceEvaluationsForManager;
        }

        public ICollection<PerformanceEvaluationInfoModel> GetPerformanceEvaluationsForReviewer(string reviewerId)
        {
            if (String.IsNullOrWhiteSpace(reviewerId))
                throw new ArgumentOutOfRangeException("reviewerId", "Reviewer's id can't be null.");
            // Each time reviewer, with id of reviewerId, logs into the system, 
            // performance evaluations of his reviewees are collected from database;
            // There are three cases: 
            // - Reviewee's performance evaluation is in 'Edit' phase (performance evaluation has already been created, but not submitted)
            // - Reviewee's performance evaluation has not been created yet, but it's not his first performance evaluation
            // - Reviewee's never had a performance evaluation
            // All of these cases are implemented below

            var usersIReview = _context.Users.Include(x => x.JobPosition)
                                            .Include(x => x.JobTitle)
                                            .Include(x => x.Template)
                                            .Include("Template.Skills")
                                            .Include("Template.TemplateDuration")
                                            .Include(x => x.Reviewer)
                                            .Where(x => x.Reviewer.Id == reviewerId)
                                            .ToList();

            List<PerformanceEvaluationInfoModel> performanceEvaluationsForReviewer = new List<PerformanceEvaluationInfoModel>();

            foreach (var user in usersIReview)
            {
                var latestPerformanceEvaluation = _context.PerformanceEvaluations.Include(x => x.JobPosition)
                                                                                    .Include(x => x.JobTitle)
                                                                                    .Include(x => x.Template)
                                                                                    .Include("Template.Skills")
                                                                                    .Where(x => x.ConsultantId == user.Id)
                                                                                    .Where(x => x.ReviewerId == reviewerId)
                                                                                    .ToList()
                                                                                    .OrderByDescending(x => x.EndDate.Date)
                                                                                    .ThenBy(x => x.EndDate.TimeOfDay)
                                                                                    .FirstOrDefault();

                var performanceEvaluationInfoModel = new PerformanceEvaluationInfoModel();
                var startDate = DateTime.MinValue;
                var endDate = DateTime.MinValue;

                if (latestPerformanceEvaluation != null)
                {
                    // If performance evaluation for the user has been already created, but it hasn't been submitted yet
                    // (user's performanceEvaluation exists in the database, with property ISubmitted = false)
                    if (!latestPerformanceEvaluation.IsSubmitted)
                    {
                        performanceEvaluationInfoModel = latestPerformanceEvaluation.MapPerformanceEvaluationToPerformanceEvaluationInfoModel();

                        startDate = latestPerformanceEvaluation.StartDate;
                        endDate = latestPerformanceEvaluation.EndDate;

                        performanceEvaluationInfoModel.InEditStage = true;
                    }
                    else if (latestPerformanceEvaluation.IsSubmitted)
                    {
                        // If user has had performance evaluation before, a new peInfoModel is created
                        // with START DATE of his latest submitted and signed pe's END DATE;
                        // END DATE value, of the new peInfoModel, is then set to sum of START DATE and TEMPLATE DURATION 
                        startDate = latestPerformanceEvaluation.EndDate;
                        endDate = startDate.AddMonths(user.Template.TemplateDuration.Duration);

                        performanceEvaluationInfoModel = user.MapApplicationUserToPerformanceEvaluationInfoModel(startDate, endDate);

                        performanceEvaluationInfoModel.InCreateStage = true;
                    }
                }
                else
                {
                    // If user has never had a performance evaluation, a new peInfoModel is created
                    // START DATE is set to user's EMPLOYMENT DATE
                    // END DATE is set to sum of user's EMPLOYMENT DATE and TEMPLATE DURATION
                    startDate = user.EmploymentDate;
                    endDate = startDate.AddMonths(user.Template.TemplateDuration.Duration);

                    performanceEvaluationInfoModel = user.MapApplicationUserToPerformanceEvaluationInfoModel(startDate, endDate);

                    performanceEvaluationInfoModel.InCreateStage = true;
                }

                if (DateTime.Now > endDate)
                    performanceEvaluationInfoModel.IsLate = true;
                else if (endDate.Subtract(DateTime.Now).TotalDays <= 10)
                    performanceEvaluationInfoModel.IsNearDueDate = true;

                performanceEvaluationsForReviewer.Add(performanceEvaluationInfoModel);
            }

            return performanceEvaluationsForReviewer
                                    .OrderByDescending(x => x.DueDate.Date)
                                    .ThenBy(x => x.DueDate.TimeOfDay)
                                    .ToList();
        }

        public ICollection<PerformanceEvaluationHistoryModel> GetPerformanceEvaluationsHistoryForManager(string managerId)
        {
            if (String.IsNullOrWhiteSpace(managerId))
                throw new ArgumentOutOfRangeException("managerId", "Manager's id can't be null.");
     
            return _context.PerformanceEvaluations.Include(x => x.JobTitle)
                                                .Include(x => x.JobPosition)
                                                .Include(x => x.Consultant)
                                                .Include(x => x.Reviewer)
                                                .Where(x => x.IsSubmitted)
                                                .Where(x => x.Consultant.ManagerId == managerId)
                                                .Where(x => x.SignedOnDateByConsultant != null)
                                                .ToList()
                                                .OrderByDescending(x => x.SubmittedDate.Value.Date)
                                                .ThenBy(x => x.SubmittedDate.Value.TimeOfDay)
                                                .Select(x => x.MapPerformanceEvaluationToPerformanceEvaluationHistoryInfoModel())
                                                .ToList();
        }

        public ICollection<PerformanceEvaluationHistoryModel> GetPerformanceEvaluationsHistoryForReviewer(string reviewerId)
        {
            if (String.IsNullOrWhiteSpace(reviewerId))
                throw new ArgumentOutOfRangeException("reviewerId", "Reviewer's id can't be null.");

            return _context.PerformanceEvaluations.Include(x => x.JobTitle)
                                                .Include(x => x.JobPosition)
                                                .Include(x => x.Consultant)
                                                .Where(x => x.ReviewerId == reviewerId)
                                                .Where(x => x.IsSubmitted)
                                                .ToList()
                                                .OrderByDescending(x => x.SubmittedDate.Value.Date)
                                                .ThenBy(x => x.SubmittedDate.Value.TimeOfDay)
                                                .Select(x => x.MapPerformanceEvaluationToPerformanceEvaluationHistoryInfoModel())
                                                .ToList();
        }

        public PerformanceEvaluationModel SaveOrSubmitPerformanceEvaluation(PerformanceEvaluationModel performanceEvaluationModel, bool isSubmit = false)
        {
            List<string> errorMessage = null;
            if (!performanceEvaluationModel.IsValid(out errorMessage, isSubmit))
                throw new ArgumentException("performanceEvaluationModel", String.Join("; ", errorMessage));

            var performanceEvaluationSkillsOfExistingPerformanceEvaluation = _context.PerformanceEvaluationsSkills
                                                                                        .Include(x => x.Skill)
                                                                                        .Where(x => x.PerformanceEvaluationId == performanceEvaluationModel.Id)
                                                                                        .ToList();

            foreach (var peSkill in performanceEvaluationModel.PerformanceEvaluationSkills)
            {
                var peSkillId = peSkill.SkillId;
                if (performanceEvaluationSkillsOfExistingPerformanceEvaluation
                    .Any(x => x.SkillId == peSkillId))
                {
                    var peSkillOfExistingPe = performanceEvaluationSkillsOfExistingPerformanceEvaluation
                                                                .Where(x => x.SkillId == peSkill.SkillId)
                                                                .FirstOrDefault();

                    if (peSkill.Grade != null && peSkillOfExistingPe.Grade != peSkill.Grade)
                        peSkillOfExistingPe.Grade = peSkill.Grade;

                    if (peSkill.Comment != null && peSkillOfExistingPe.PESkillComment != peSkill.Comment)
                        peSkillOfExistingPe.PESkillComment = peSkill.Comment;
                }
                else
                {
                    if (peSkill.Grade != null || peSkill.Comment != null)
                    {
                        // Add new entry in PerformanceEvaluationSkill table if reviewer wrote a comment or a grade
                        var performanceEvaluationSkillModel = new PerformanceEvaluationSkillModel
                        {
                            PerformanceEvaluationId = performanceEvaluationModel.Id,
                            SkillId = peSkill.SkillId,
                            Grade = peSkill.Grade,
                            Comment = peSkill.Comment
                        };

                        var performanceEvaluationSkill = performanceEvaluationSkillModel
                            .MapPerformanceEvaluationSkillModelToPerformanceEvaluationSkill();

                        _context.PerformanceEvaluationsSkills.Add(performanceEvaluationSkill);

                        _context.SaveChanges();

                        performanceEvaluationSkillModel.Id = performanceEvaluationSkill.PerformanceEvaluationId;
                    }
                }
            }
            //Include users and return performance evaluation
            var performanceEvaluation = _context.PerformanceEvaluations
                                                    .Include(x=>x.Consultant)
                                                    .Include(x=>x.Reviewer)
                                                    .Where(x => x.PerformanceEvaluationId == performanceEvaluationModel.Id)
                                                    .FirstOrDefault();

            if (isSubmit)
            {
                performanceEvaluation.IsSubmitted = true;
                performanceEvaluation.SubmittedDate = DateTime.Now;
                _context.SaveChanges();
            }

            return performanceEvaluation.MapPerformanceEvaluationToPerformanceEvaluationModel(true);
            
        }

        public bool AcknowledgePerformanceEvaluation(PerformanceEvaluationModel performanceEvaluationModel)
        {
            if (String.IsNullOrEmpty(performanceEvaluationModel.ConsultantComment))
                throw new ArgumentException("Comment must be written!");

            var performanceEvaluation = _context.PerformanceEvaluations
                                                        .Where(x => x.PerformanceEvaluationId == performanceEvaluationModel.Id)
                                                        .FirstOrDefault();

            performanceEvaluation.ConsultantComment = performanceEvaluationModel.ConsultantComment;
            performanceEvaluation.SignedOnDateByConsultant = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public PerformanceEvaluationModel CreatePerformanceEvaluation(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                throw new ArgumentOutOfRangeException("userId", "User's id can't be null.");

            ApplicationUserModel userModel = _context.Users.Include(x => x.JobPosition)
                                                            .Include(x => x.JobTitle)
                                                            .Include(x => x.Reviewer)
                                                            .Include(x => x.Template)
                                                            .Include("Template.TemplateDuration")
                                                            .Include("Template.Skills")
                                                            .Where(x => x.Id == userId)
                                                            .FirstOrDefault()
                                                            .MapApplicationUserToApplicationUserModel(true,true,false,true,true,true);


            var latestPerformanceEvaluation = _context.PerformanceEvaluations.Where(x => x.ConsultantId == userId)
                                                                            .OrderByDescending(x => x.PerformanceEvaluationId)
                                                                            .FirstOrDefault();

            // Create PerformanceEvaluationModel based on ApplicationUserModel
            var performanceEvaluationModel = userModel.MapApplicationUserModelToPerformanceEvaluationModel();

            if (latestPerformanceEvaluation != null)
            {
                performanceEvaluationModel.StartDate = latestPerformanceEvaluation.EndDate;
                performanceEvaluationModel.EndDate = latestPerformanceEvaluation.EndDate.AddMonths(userModel.Template.TemplateDuration.Duration);
            }
            else
            {
                performanceEvaluationModel.StartDate = userModel.EmploymentDate;
                performanceEvaluationModel.EndDate = userModel.EmploymentDate.AddMonths(userModel.Template.TemplateDuration.Duration);
            }

            // Map PerformanceEvaluationInfoModel to PerformanceEvaluation to add new PE to database
            var performanceEvaluation = performanceEvaluationModel.MapPerformanceEvaluationModelToPerformanceEvaluation();

            _context.PerformanceEvaluations.Add(performanceEvaluation);
            _context.SaveChanges();

            performanceEvaluationModel.Id = performanceEvaluation.PerformanceEvaluationId;

            return performanceEvaluationModel;
        }
    }
}

