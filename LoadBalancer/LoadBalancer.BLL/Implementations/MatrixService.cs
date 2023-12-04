using Humanizer;
using LoadBalancer.BLL.Interfaces;
using LoadBalancer.DAL.Interfaces;
using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Enum;
using LoadBalancer.Domain.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BLL.Implementations
{
    public class MatrixService : IMatrixService
    {
        private readonly IBaseRepository<Matrix> _matrixRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MatrixService(IBaseRepository<Matrix> matrixRepository, IBaseRepository<User> userRepository)
        {
            _matrixRepository = matrixRepository;
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<Matrix>> GenerateRandomMatrix(Matrix matrix, User user)
        {
            try
            {                                              
                if (matrix == null || user == null)
                {
                    matrix.Status = HeavyTaskStatus.FAIL;
                    return new BaseResponse<Matrix>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.ObjectNotFound
                    };
                }
                else
                {                    
                    matrix.Status = HeavyTaskStatus.INIT;
                    matrix.Guid = Guid.NewGuid();
                    var UserEmail = user.Email;
                    if (UserEmail == null)
                    {
                        matrix.Status = HeavyTaskStatus.FAIL;
                        return new BaseResponse<Matrix>()
                        {
                            Description = "User ID not found or invalid",
                            StatusCode = StatusCode.ObjectNotFound
                        };
                    }
                    else
                    {
                        matrix.UserEmail = UserEmail.ToString();                        
                    }

                    await _matrixRepository.Create(matrix);
                }

                matrix.Status = HeavyTaskStatus.RUN;                
                var random = new Random();                
                int rows = matrix.Rows;
                int columns = matrix.Columns;
                int min = matrix.Min;
                int max = matrix.Max;
                var generatedMatrix = new List<List<int>>();

                for (int i = 0; i < rows; i++)
                {
                    var row = new List<int>();
                    for (int j = 0; j < columns; j++)
                    {
                        int value = random.Next(min, max + 1);
                        row.Add(value);
                        matrix.Progress = (i * columns + j + 1) / (double)(rows * columns) * 100;
                        await _matrixRepository.Update(matrix);
                    }
                    generatedMatrix.Add(row);
                }

                matrix.GeneratedMatrix = JsonConvert.SerializeObject(generatedMatrix);

                matrix.Status = HeavyTaskStatus.SUCCESS;
                await _matrixRepository.Update(matrix);
                return new BaseResponse<Matrix>()
                {
                    StatusCode = StatusCode.OK,
                    Data = matrix
                };
            }
            catch (Exception ex)
            {
                matrix.Status = HeavyTaskStatus.FAIL;
                return new BaseResponse<Matrix>()
                {
                    Description = $"[Matrix] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task <BaseResponse<Matrix>> GetTask(Guid guid)
        {
            try
            {
                var task = await _matrixRepository.GetAll().FirstOrDefaultAsync(x => x.Guid == guid);

                if (task == null)
                {
                    return new BaseResponse<Matrix>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.ObjectNotFound
                    };
                }

                return new BaseResponse<Matrix>()
                {
                    StatusCode = StatusCode.OK,
                    Data = task
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Matrix>()
                {
                    Description = $"[GetTask] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<Matrix>>> GetTasks(User user)
        {
            try
            {
                var tasks = await _matrixRepository.GetAll()
                    .Where(u => u.UserEmail == user.Email)
                    .ToListAsync();

                if (!tasks.Any())
                {
                    return new BaseResponse<List<Matrix>>()
                    {
                        Description = "0 elements found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Matrix>>()
                {
                    Data = tasks,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Matrix>>()
                {
                    Description = $"[GetTasks] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteTask(Guid guid)
        {
            try
            {
                var task = await _matrixRepository.GetAll().FirstOrDefaultAsync(x => x.Guid == guid);

                if (task == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.ObjectNotFound,
                        Data = false
                    };
                }

                await _matrixRepository.Delete(task);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}