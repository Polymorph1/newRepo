using PerformanceEvaluationPortal.BLL.Interfaces;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace PerformanceEvaluationPortal.WebAPI.Controllers
{
    [Authorize]
    public class ApplicationUsersController : ApiController
    {
        private IApplicationUserService _applicationUserService;
        private IAuthorizeService _authorizeService;
        public ApplicationUsersController(IApplicationUserService applicationUserService, IAuthorizeService authorizeService)
        {
            _applicationUserService = applicationUserService;
            _authorizeService = authorizeService;
        }

        // GET: api/ApplicationUsers
        public IHttpActionResult Get()
        {
            var users = _applicationUserService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/GetUserByUsername/{username}")]
        public IHttpActionResult GetUserByUsername(string username)
        {
            var user = _applicationUserService.GetUserByUsername(username);
            return Ok(user);
        }

        // GET: api/ApplicationUsers/5
        [Authorize(Roles = "Admin")]
        [Route("api/GetUserById/{id}")]
        public IHttpActionResult GetUserById(string id)
        {
            var user = _applicationUserService.GetUser(id);
            return Ok(user);

        }

        [Route("api/GetLoggedUser")]
        public IHttpActionResult GetLoggedUser()
        {
            var id = User.Identity.GetUserId();

            if (String.IsNullOrEmpty(id))
                return BadRequest("User is not logged on.");

            var user = _applicationUserService.GetLoggedUser(id);
            return Ok(user);
        }

        //Authorize for reviewer
        [Route("api/GetMyTeamReviewer")]
        public IHttpActionResult GetMyTeamReviewer()
        {

            var id = User.Identity.GetUserId();
            if (_authorizeService.CheckAuthorize(id) != 3)
            { 
                if (_authorizeService.CheckAuthorize(id) == 1 || _authorizeService.CheckAuthorize(id) == 0)
                    throw new UnauthorizedAccessException("Unauthorized access");
            }
            var user = _applicationUserService.GetMyReviewees(id);
            return Ok(user);

        }

       //Authorize for manager
        [Route("api/GetMyTeamManager")]
        public IHttpActionResult GetMyTeamManager()
        {
   
            var id = User.Identity.GetUserId();
            if (_authorizeService.CheckAuthorize(id) != 3)
            {
                if (_authorizeService.CheckAuthorize(id) == 2 || _authorizeService.CheckAuthorize(id) == 0)
                    throw new UnauthorizedAccessException("Unauthorized access");
                //return Unauthorized();
            }
            var user = _applicationUserService.GetMyTeam(id);
            return Ok(user);      
         
            
        }

        // PUT: api/ApplicationUsers/5
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/ApplicationUsers/{id}")]
        public IHttpActionResult Put(string id, [FromBody]ApplicationUserModel user)
        {
           var updatedUser = _applicationUserService.UpdateUser(id, user);

            return Ok(updatedUser);
        }
    }
}
