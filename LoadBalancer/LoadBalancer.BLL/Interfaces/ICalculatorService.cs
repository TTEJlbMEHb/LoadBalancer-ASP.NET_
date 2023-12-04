using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BLL.Interfaces
{
    public interface ICalculatorService
    {
        Task<IBaseResponse<Calculator>> PerformCalculation(Calculator calculator, string operation);
    }

}
