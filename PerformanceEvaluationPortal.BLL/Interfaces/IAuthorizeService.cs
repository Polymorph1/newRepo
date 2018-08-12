using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceEvaluationPortal.BLL.Interfaces
{
    public interface IAuthorizeService
    {
        int CheckAuthorize(string userId);
       

    }
}
