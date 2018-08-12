using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class JobTitleMapper
    {
        public static JobTitleModel MapJobTitleToJobTitleModel(this JobTitle jobTitle)
        {
            if (jobTitle == null)
                return null;

            var jobTitleModel = new JobTitleModel
            {
                Id = jobTitle.JobTitleId,
                Name = jobTitle.Name
            };

            return jobTitleModel;
        }
        //TO DO:Change or Remove
        public static JobTitle MapJobTitleModelToJobTitle(this JobTitleModel jobTitleModel)
        {
            if (jobTitleModel == null)
                return null;

            var jobTitle = new JobTitle
            {
                JobTitleId = jobTitleModel.Id,
                Name = jobTitleModel.Name
            };

            return jobTitle;
        }
    }
}
