using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class JobPostionMapper
    {
        public static JobPositionModel MapJobPositionToJobPositionModel(this JobPosition jobPosition)
        {
            if (jobPosition == null)
                return null;

            var jobPositionModel = new JobPositionModel
            {
                Id = jobPosition.JobPositionId,
                Name = jobPosition.PositionName
            };

            return jobPositionModel;
        }
        //TO DO:Remove or change
        public static JobPosition MapJobPositionModelToJobPosition(this JobPositionModel jobPositionModel)
        {
            if (jobPositionModel == null)
                return null;

            var jobPosition = new JobPosition
            {
                JobPositionId = jobPositionModel.Id,
                PositionName = jobPositionModel.Name
            };

            return jobPosition;
        }
    }
}
