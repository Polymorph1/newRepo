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
    public class JobPositionsController : ApiController
    {
        private IJobPositionService _jobPositionService;
        public JobPositionsController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        // GET: api/JobPositions
        public IHttpActionResult Get()
        {
            var jobPositions = _jobPositionService.GetJobPositions();
            return Ok(jobPositions);
        }


    }
}
