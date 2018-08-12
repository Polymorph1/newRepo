using PerformanceEvaluationPortal.BLL.Interfaces;
using PerformanceEvaluationPortal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerformanceEvaluationPortal.DTO;
using PerformanceEvaluationPortal.Common.Mappers;
using System.Data.Entity;

namespace PerformanceEvaluationPortal.BLL
{
    public class NotificationService:INotificationService
    {
        private ApplicationDbContext _context;
  
        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public NotificationModel CreateNotification(NotificationModel notificationModel)
        {
            var newNotification = notificationModel.MapNotificationModelToNotification();

            _context.Notifications.Add(newNotification);

            _context.SaveChanges();

            notificationModel.Id = newNotification.Id;

            return notificationModel;
        }

        public ICollection<NotificationModel> GetNotificationByReceiver(string userId)
        {
            var notifications = _context.Notifications
                .Where(x=>x.ReceiverId==userId)
                .ToList();
            return notifications.Select(x=>x.MapNotificationToNotificationModel()).ToList();
        }

        public ICollection<NotificationModel> GetNotificationByUserName(string userName)
        {
            var notifications = _context.Notifications
               .Where(x => x.ReceiverUserName== userName)
               .Where(x=>x.Seen==false)
               .ToList();
            return notifications.Select(x => x.MapNotificationToNotificationModel()).ToList();
        }
       public int GetNotificationNumber(string userName)
        {
            return _context.Notifications
               .Where(x => x.ReceiverUserName == userName)
               .Where(x => x.Seen == false).Count();

        }
        public ICollection<NotificationModel> GetNotifications()
        {
            var notifications = _context.Notifications
                .ToList();
            return notifications.Select(x => x.MapNotificationToNotificationModel()).ToList();
        }

        public int? GetNotificationByPeId(int PeId)
        {

            var notification= _context.Notifications
               .Where(x => x.PeId == PeId)
               .Where(x => x.Seen == false).FirstOrDefault();

            return notification.Id;

        }

        public void SetSeenOfNotification(int id)
        {
            var notification = _context.Notifications.Where(x => x.Id == id).FirstOrDefault();
            notification.Seen = true;
            _context.SaveChanges();
        }
    }
}
