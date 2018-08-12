using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.Common.Mappers;

namespace PerformanceEvaluationPortal.BLL
{
    public class JobTitleService : IJobTitleService
    {        
        private ApplicationDbContext _context;
        public JobTitleService(ApplicationDbContext context)
        {
            _context = context;
        }
         public ICollection<JobTitleModel> GetJobTitles()
        {
            var jobTitles = _context.JobTitles.ToList();

            return jobTitles.Select(x => x.MapJobTitleToJobTitleModel())
                            .ToList();
        }
    }
}
