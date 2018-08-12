using PerformanceEvaluationPortal.DomainModel;
using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.Common.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationModel MapNotificationToNotificationModel(this Notification notification)
        {
            if (notification == null)
                return null;

            var notificationModel = new NotificationModel
            {
                Id = notification.Id,
                PeId=notification.PeId,
                Sender=notification.Sender,
                ReceiverId=notification.ReceiverId,
                ReceiverUserName=notification.ReceiverUserName,
                Message=notification.Message,
                Seen=notification.Seen,   

            };

            return notificationModel;

        }
        public static Notification MapNotificationModelToNotification(this NotificationModel notificationModel)
        {
            if (notificationModel == null)
                return null;

            var notification = new Notification
            {
                Id = notificationModel.Id,
                PeId=notificationModel.PeId,
                Sender=notificationModel.Sender,
                ReceiverId=notificationModel.ReceiverId,
                ReceiverUserName=notificationModel.ReceiverUserName,
                Message=notificationModel.Message,
                Seen=notificationModel.Seen
            };
            return notification;

        }
    }
}
