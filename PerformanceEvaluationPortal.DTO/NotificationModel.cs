using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.DTO
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverUserName { get; set; }
        public bool Seen { get; set; }
        public int? PeId { get; set; }
        public string Message { get; set; }

    }
}
