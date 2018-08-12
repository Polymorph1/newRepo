using PerformanceEvaluationPortal.BLL;
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
    public class TemplateDurationsController : ApiController
    {
        private ITemplateDurationService _templateDurationService;

        public TemplateDurationsController(ITemplateDurationService templateDurationService)
        {
            _templateDurationService = templateDurationService;

        }

        // GET: api/TemplateDurations
        public IHttpActionResult Get()
        {
            var templateDurations = _templateDurationService.GetAllTemplateDurations();

            return Ok(templateDurations);
        }

    }
}
