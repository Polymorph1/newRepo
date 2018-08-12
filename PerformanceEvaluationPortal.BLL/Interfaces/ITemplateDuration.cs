using PerformanceEvaluationPortal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.BLL.Interfaces
{
    public  interface ITemplateDurationService
    {
        ICollection<TemplateDurationModel> GetAllTemplateDurations();
    }
}
