using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PerformanceEvaluationPortal.DAL;
using PerformanceEvaluationPortal.DTO;

namespace PerformanceEvaluationPortal.WebAPI.Notifications
{
  
    public class NotificationHub : Hub
    {

      

        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
           
        }

        public void LeaveGroup(string groupName)
        {
            Groups.Remove(Context.ConnectionId, groupName);
        }
     
        public void NotifyConsultant(string groupName,NotificationModel notification)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            context.Clients.Group(notification.ReceiverUserName).displayNotificationConsultant(notification);
          

        }


    }
   

}