using PerformanceEvaluationPortal.BLL.Interfaces;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PerformanceEvaluationPortal.WebAPI.Notifications;

namespace PerformanceEvaluationPortal.WebAPI.Controllers
{   
    public class PerformanceEvaluationsController : ApiController
    {
        private IPerformanceEvalutionService _performanceEvaluationService;
        private IAuthorizeService _authorizeService;
        private INotificationService _notificationService;
        private IHubContext _context;
        public PerformanceEvaluationsController(IPerformanceEvalutionService performanceEvaluationService,
            IAuthorizeService authorizeService,INotificationService notificationService)
        {
            _performanceEvaluationService = performanceEvaluationService;
            _authorizeService = authorizeService;
            _notificationService = notificationService;
            _context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        }

        [System.Web.Http.Authorize]
        [Route("api/GetPerformanceEvaluationsById")]
        public IHttpActionResult GetPerformanceEvaluationById(int performanceEvaluationId)
        {
            var performanceEvaluations = _performanceEvaluationService.GetPerformanceEvaluation(performanceEvaluationId);
            return Ok(performanceEvaluations);
        }
        //Authorize manager
        [System.Web.Http.Authorize]
        [Route("api/GetPerformanceEvaluationsForManager")]
        public IHttpActionResult GetPerformanceEvaluationsForManager()
        {
            string managerId = User.Identity.GetUserId();
            if (_authorizeService.CheckAuthorize(managerId) != 3)
            {
                if (_authorizeService.CheckAuthorize(managerId) == 2 || _authorizeService.CheckAuthorize(managerId) == 0)
                    throw new UnauthorizedAccessException("Unauthorized access");
                //return Unauthorized();
            }

            var performanceEvaluations = _performanceEvaluationService.GetLatePerformanceEvaluationsForManager(managerId);
            return Ok(performanceEvaluations);
           
            
        }
        //Authorize reviewer
        [System.Web.Http.Authorize]
        [Route("api/GetPerformanceEvaluationsForReviewer")]
        public IHttpActionResult GetPerformanceEvaluationsForReviewer()
        {
            string reviewerId = User.Identity.GetUserId();
            if (_authorizeService.CheckAuthorize(reviewerId) != 3)
            {
                if (_authorizeService.CheckAuthorize(reviewerId) == 1 || _authorizeService.CheckAuthorize(reviewerId) == 0)
                    throw new UnauthorizedAccessException("Unauthorized access");
            }
            var performanceEvaluations = _performanceEvaluationService.GetPerformanceEvaluationsForReviewer(reviewerId);

            performanceEvaluations = performanceEvaluations.Where(x => x.IsLate || x.IsNearDueDate)
                                                           .ToList();

            return Ok(performanceEvaluations);
        }

        //Authorize reviewer
        [System.Web.Http.Authorize]
        [Route("api/GetPerformanceEvaluationHistoryForReviewer")]
        public IHttpActionResult GetPerformanceEvaluationHistoryForReviewer()
        {
            string reviewerId = User.Identity.GetUserId();

            if (_authorizeService.CheckAuthorize(reviewerId) != 3)
            {
                if (_authorizeService.CheckAuthorize(reviewerId) == 1 || _authorizeService.CheckAuthorize(reviewerId) == 0)
                    throw new UnauthorizedAccessException("Unauthorized access");
            }
            var performanceEvaluations = _performanceEvaluationService.GetPerformanceEvaluationsHistoryForReviewer(reviewerId);

            return Ok(performanceEvaluations);
        }
        //Authorize manager
        [System.Web.Http.Authorize]
        [Route("api/GetPerformanceEvaluationHistoryForManager")]
        public IHttpActionResult GetPerformanceEvaluationHistoryForManager()
        {
            string managerId = User.Identity.GetUserId();

            if (_authorizeService.CheckAuthorize(managerId) != 3)
            {
                if (_authorizeService.CheckAuthorize(managerId) == 2 || _authorizeService.CheckAuthorize(managerId) == 0)
                    throw new UnauthorizedAccessException("Unauthorized access");
                // return Unauthorized();
            }
            var performanceEvaluations = _performanceEvaluationService.GetPerformanceEvaluationsHistoryForManager(managerId);

             return Ok(performanceEvaluations);
           
            
        }

        [System.Web.Http.Authorize]
        [Route("api/GetMyPerformanceEvaluations")]
        public IHttpActionResult GetMyPerformanceEvaluations(bool signed = false)
        {
            string userId = User.Identity.GetUserId();

            var performanceEvaluations = _performanceEvaluationService.GetMyPerformanceEvaluations(userId, signed);

            return Ok(performanceEvaluations);
        }

        [System.Web.Http.Authorize]
        [HttpPost]
        [Route("api/CreatePerformanceEvaluation/{userId}")]
        public IHttpActionResult CreatePerformanceEvaluation(string userId)
        {
            var performanceEvaluations = _performanceEvaluationService.CreatePerformanceEvaluation(userId);

            return Ok(performanceEvaluations);
        }

        [System.Web.Http.Authorize]
        [HttpPut]
        [Route("api/SubmitPerformanceEvaluation")]
        public IHttpActionResult SubmitPerformanceEvaluation([FromBody]PerformanceEvaluationModel performanceEvaluationModel)
        {
            var performanceEvaluations = _performanceEvaluationService.SaveOrSubmitPerformanceEvaluation(performanceEvaluationModel, true);
            NotificationModel notification = new NotificationModel();
            notification.PeId = performanceEvaluations.Id;
            notification.ReceiverId = performanceEvaluations.ReviewerId;
            notification.ReceiverUserName = performanceEvaluations.Consultant.Username;
            notification.Sender = performanceEvaluations.ReviewerFirstName + " " + performanceEvaluations.ReviewerLastName;
            notification.Seen = false;
            notification.Message="You have review to acknowledge submited by ";
           
            var created_notif = _notificationService.CreateNotification(notification);

            _context.Clients.Group(created_notif.ReceiverUserName).displayNotificationConsultant(created_notif);

            return Ok(performanceEvaluations);
        }

        [System.Web.Http.Authorize]
        [HttpPut]
        [Route("api/SavePerformanceEvaluation")]
        public IHttpActionResult SavePerformanceEvaluation([FromBody]PerformanceEvaluationModel performanceEvaluationModel)
        {
            var performanceEvaluations = _performanceEvaluationService.SaveOrSubmitPerformanceEvaluation(performanceEvaluationModel);

            return Ok(performanceEvaluations);
        }

        [System.Web.Http.Authorize]
        [HttpPut]
        [Route("api/AcknowledgePerformanceEvaluation")]
        public IHttpActionResult AcknowledgePerformanceEvaluation([FromBody]PerformanceEvaluationModel peModel)
        {
            var performanceEvaluations = _performanceEvaluationService.AcknowledgePerformanceEvaluation(peModel);

            return Ok(performanceEvaluations);
        }
    }
}
