using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BLL.Interfaces
{
    public interface IMatrixService
    {
        Task<IBaseResponse<Matrix>> GenerateRandomMatrix(Matrix matrix, User user);
        Task<BaseResponse<Matrix>> GetTask(Guid guid);
        Task<IBaseResponse<List<Matrix>>> GetTasks(User user);
        Task<IBaseResponse<bool>> DeleteTask(Guid guid);
    }
}
