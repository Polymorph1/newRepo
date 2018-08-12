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
    public class JobPositionService : IJobPositionService
    {
        private ApplicationDbContext _context;
        public JobPositionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<JobPositionModel> GetJobPositions()
        {
            var jobPositions = _context.JobPositions.ToList();

            return jobPositions.Select(x => x.MapJobPositionToJobPositionModel())
                                .ToList();
        }
    }
}
