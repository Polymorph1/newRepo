using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.Common.Mappers;
using PerformanceEvaluationPortal.Common.Validation;
using System.Data.Entity;
using PerformanceEvaluationPortal.DomainModel;

namespace PerformanceEvaluationPortal.BLL
{
    public class TemplateService : ITemplateService
    {
        private ApplicationDbContext _context;
        public TemplateService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool ArchiveTemplate(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", "Id must be positive number.");

            var template = _context.Templates.
                                            Where(x => x.TemplateId == id)
                                            .Include(x => x.ApplicationsUsers)
                                            .FirstOrDefault();

            var templateModel = new TemplateModel();

            if (template == null)
                throw new ApplicationException(String.Format("Template with id {0} does not exist", id));

            templateModel = template.MapTemplateToTemplateModel(false);

            templateModel.canBeArchived = template.ApplicationsUsers != null && template.ApplicationsUsers.Count > 0;

            if (!templateModel.canBeArchived)
            {
                template.IsArchived = !template.IsArchived;

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException(String.Format("Template cant be archived"));
            }
                        

            return template.IsArchived;
        }

        public TemplateModel CreateTemplate(TemplateModel templateModel)
        {
            List<string> errorMessage = null;

            if (!templateModel.IsValid(out errorMessage))
            {
                throw new ArgumentException("templateModel", String.Join("; ", errorMessage));
            }
          
            
            
            var template = templateModel.MapTemplateModelToTemplate();

            List<int> skillsToAdd = templateModel.Skills.Select(x => x.Id).ToList();

            var skillsFromContext = _context.Skills.
                                                    Where(x => skillsToAdd.Contains(x.SkillId)).
                                                    ToList();

            template.Skills = skillsFromContext;

            _context.Templates.Add(template);

            _context.SaveChanges();

            templateModel.Id = template.TemplateId;

            return templateModel;
           
        }

        public ICollection<TemplateModel> GetArchivedTemplates()
        {
            var templates = _context.Templates.ToList();

            return templates.
                            Select(x => x.MapTemplateToTemplateModel(false)).
                            Where(x => x.IsArchived == true)
                            .OrderBy(x=>x.Name).ToList();
        }

        public TemplateModel GetTemplate(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", "Id must be positive number.");
           
            var template = _context.Templates
                .Where(x=>x.TemplateId ==id)
                .Include(x=>x.Skills)
                .Include(x=>x.TemplateDuration)
                .Include(x=>x.ApplicationsUsers)
                .Include(x => x.PerformanceEvaluations)
                .FirstOrDefault();
            
            if (template == null)
                throw new ApplicationException(String.Format("Template with id {0} does not exist", id));

            var templateModel = new TemplateModel();
            templateModel = template.MapTemplateToTemplateModel(true);
            templateModel.canBeEdited = template.PerformanceEvaluations != null && template.PerformanceEvaluations
                                                                                           .Where(x => x.IsSubmitted == false).Count() > 0;

            return templateModel;
        }

        public ICollection<TemplateModel> GetActiveTemplates()
        {
            var templates = _context.Templates
                .Include(x => x.ApplicationsUsers)
                .Include(x => x.PerformanceEvaluations)
                .ToList();

            var templateModel = new List<TemplateModel>();

            templates = templates
                            .Where(x => x.IsArchived == false).ToList();
           
            templateModel = templates.Select(x => x.MapTemplateToTemplateModel(false)).ToList();

            for (var i = 0; i < templateModel.Count; i++)
            {
                templateModel[i].canBeArchived = (templates[i].ApplicationsUsers != null && templates[i].ApplicationsUsers.Count > 0);

                templateModel[i].canBeEdited = (templates[i].PerformanceEvaluations != null && templates[i].PerformanceEvaluations.Where(x => x.IsSubmitted == false).Count() > 0);
            }

            return templateModel.OrderBy(x=>x.Name).ToList();
        }

        public TemplateModel UpdateTemplate(int templateId, TemplateModel templateModel)
        {
            List<string> errorMessage = null;

            if (!templateModel.IsValid(out errorMessage))
            {
                throw new ArgumentException("templateModel", String.Join("; ", errorMessage));
            }

            var template = _context.Templates
                                              .Include(x => x.Skills)
                                              .Include(x => x.ApplicationsUsers)
                                              .Include(x => x.PerformanceEvaluations)
                                              .Where(x => x.TemplateId == templateId)
                                              .FirstOrDefault();    
              
            if (template == null)
                throw new ApplicationException(String.Format("Template with id {0} does not exist", templateModel.Id));

             templateModel.canBeEdited = template.PerformanceEvaluations != null && template.PerformanceEvaluations
                                                                                           .Where(x => x.IsSubmitted == false).Count() > 0;

            if (!templateModel.canBeEdited)
            {

                template = templateModel.MapTemplateModelToTemplate(template);


                List<int> skillsToAdd = templateModel.Skills.Select(x => x.Id).ToList();

                template.Skills.Clear();


                var skillsFromContext = _context.Skills.Where(x => skillsToAdd.Contains(x.SkillId)).ToList();

                template.Skills = skillsFromContext;

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException(String.Format("Template cant be edited"));
            }

            return template.MapTemplateToTemplateModel(true);

        }
    }
}
