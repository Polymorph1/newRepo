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
    public class SkillsController : ApiController
    {
        private ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;

        }

        // GET: api/Skills
        public IHttpActionResult Get()
        {
            var skills = _skillService.GetAllSkills();

            return Ok(skills);
        }

    }
}
