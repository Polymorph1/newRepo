using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.Common;

namespace PerformanceEvaluationPortal.BLL
{
    public class TemplateDurationService : ITemplateDurationService
    {
        private ApplicationDbContext _context;
        public TemplateDurationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<TemplateDurationModel> GetAllTemplateDurations()
        {           
             var templateDurations = _context.TemplateDurations.ToList();
            return templateDurations.
                                    Select(x => x.MapTemplateDurationToTemplateDurationModel()).
                                    ToList();
        }
    }
}
