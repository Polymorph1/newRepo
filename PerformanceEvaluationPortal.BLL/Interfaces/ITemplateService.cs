using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.BLL.Interfaces
{
    public interface ITemplateService
    {
        TemplateModel CreateTemplate(TemplateModel templateModel);
        TemplateModel GetTemplate(int id);
        ICollection<TemplateModel> GetActiveTemplates();
        ICollection<TemplateModel> GetArchivedTemplates();
        TemplateModel UpdateTemplate(int templateId, TemplateModel templateModel);
        bool ArchiveTemplate(int id);
    }
}
