using PerformanceEvaluationPortal.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerformanceEvaluationPortal.WebAPI.Controllers
{
    [Authorize]
    public class JobTitlesController : ApiController
    {
        private IJobTitleService _jobTitleService;

        public JobTitlesController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }

        // GET: api/JobTitles
        public IHttpActionResult Get()
        {
            var jobTitles = _jobTitleService.GetJobTitles();
            return Ok(jobTitles);
        }
    }
}
