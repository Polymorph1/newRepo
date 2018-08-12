using Microsoft.AspNet.Identity;
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
    public class NotificationController : ApiController
    {
        private INotificationService _notificationService;
   
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
            
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
       
            var notifications = _notificationService.GetNotifications();
            return Ok(notifications);
        }

        [Route("api/GetByName")]
        public IHttpActionResult GetByName()
        {
            var userName = User.Identity.Name;
            var notifications = _notificationService.GetNotificationByUserName(userName);
            return Ok(notifications);
        }
        [HttpGet]
        [Route("api/GetNotificationNumber")]
        public int GetNotificationNumber()
        {
            var userName = User.Identity.Name;
           return _notificationService.GetNotificationNumber(userName);
            
        }
        [Route("api/GetByReceiver")]
        public IHttpActionResult GetByReceiver()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _notificationService.GetNotificationByReceiver(userId);
            return Ok(notifications);
        }

        [HttpGet]
        [Route("api/GetNotificationByPeId/{PeId}")]
        public int? GetNotificationByPeId(int PeId)
        {
            return _notificationService.GetNotificationByPeId(PeId);

        }

        [HttpPut]
        [Route("api/SetSeenStatus/{id}")]
        public void SetSeenStatus(int id)
        {
            _notificationService.SetSeenOfNotification(id);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]NotificationModel notification)
        {
            var createdNotif = _notificationService.CreateNotification(notification);
            return Ok(createdNotif);
        }
       
    }
}
