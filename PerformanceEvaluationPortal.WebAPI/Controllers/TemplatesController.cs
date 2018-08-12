using Microsoft.AspNet.SignalR;
using PerformanceEvaluationPortal.BLL.Interfaces;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.WebAPI.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PerformanceEvaluationPortal.WebAPI.Controllers
{
    [System.Web.Http.Authorize]
    public class TemplatesController : ApiController
    {
        private ITemplateService _templateService;
        private INotificationService _notificationService;

        public TemplatesController(ITemplateService templateService,INotificationService notificationService)
        {
            _templateService = templateService;           
            _notificationService = notificationService;

        }

        // GET: api/Templates
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            
            var templates = _templateService.GetActiveTemplates();
            return Ok(templates);
        }

        // GET: api/Templates/5
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IHttpActionResult Get(int id)
        {
           
            var template = _templateService.GetTemplate(id);
            return Ok(template);

        }

        // POST: api/Templates
        [System.Web.Http.Authorize(Roles ="Admin")]
        public IHttpActionResult Post([FromBody]TemplateModel template)
        {
            if (template == null)
            {
                return BadRequest("Template model must be provided");
            }

            var createdTemplate = _templateService.CreateTemplate(template);
          
            return Ok(createdTemplate);
        }
        // PUT: api/Templates/5
        [System.Web.Http.Authorize(Roles = "Admin")]
        public IHttpActionResult Put(int id, [FromBody]TemplateModel template)
        {

            var updatedTemplate = _templateService.UpdateTemplate(id, template);

            return Ok(updatedTemplate);
        }

        [System.Web.Http.Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/GetArchivedTemplates")]
        public IHttpActionResult GetArchivedTemplates()
        {

            var templates = _templateService.GetArchivedTemplates();

            return Ok(templates);
        }

        [System.Web.Http.Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/ArchiveTemplate/{id}")]
        public IHttpActionResult ArchiveTemplate(int id)
        {

            var templates = _templateService.ArchiveTemplate(id);

            return Ok(templates);
        }
    }
}
