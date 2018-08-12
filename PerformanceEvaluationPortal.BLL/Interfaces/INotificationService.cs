using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.BLL.Interfaces
{
    public interface INotificationService
    {
        ICollection<NotificationModel> GetNotifications();
        NotificationModel CreateNotification(NotificationModel notification);
        ICollection<NotificationModel> GetNotificationByReceiver(string userId);
        ICollection<NotificationModel> GetNotificationByUserName(string userName);
        int GetNotificationNumber(string userName);
        void SetSeenOfNotification(int id);
        int? GetNotificationByPeId(int PeId);

    }
}
