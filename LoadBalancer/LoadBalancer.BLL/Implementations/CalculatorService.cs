using LoadBalancer.BLL.Interfaces;
using LoadBalancer.DAL.Interfaces;
using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Enum;
using LoadBalancer.Domain.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BLL.Implementations
{
    public class CalculatorService : ICalculatorService
    {
        public async Task<IBaseResponse<Calculator>> PerformCalculation(Calculator calculator, string operation)
        {
            try
            {
                calculator.Status = HeavyTaskStatus.INIT;

                if (calculator == null)
                {
                    calculator.Status = HeavyTaskStatus.FAIL;
                    return new BaseResponse<Calculator>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.ObjectNotFound
                    };
                }

                calculator.Status = HeavyTaskStatus.RUN;
                var x = calculator.X;
                var y = calculator.Y;

                switch (operation)
                {
                    case "add":
                        calculator.Result = await Add(x, y);
                        break;
                    case "minus":
                        calculator.Result = await Subtract(x, y);
                        break;
                    case "multiply":
                        calculator.Result = await Multiply(x, y);
                        break;
                    case "divide":
                        calculator.Result = await Divide(x, y);
                        break;
                    default:
                        throw new ArgumentException("Invalid operation");
                }

                calculator.Status = HeavyTaskStatus.SUCCESS;

                return new BaseResponse<Calculator>()
                {
                    StatusCode = StatusCode.OK,
                    Data = calculator
                };
            }
            catch (Exception ex)
            {
                calculator.Status = HeavyTaskStatus.FAIL;
                return new BaseResponse<Calculator>()
                {
                    Description = $"[Calculation] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };                
            }
        }

        private async Task<double> Add(double x, double y)
        {
            return x + y;
        }

        private async Task<double> Subtract(double x, double y)
        {
            return x - y;
        }

        private async Task<double> Multiply(double x, double y)
        {
            return x * y;
        }

        private async Task<double> Divide(double x, double y)
        {
            if (y != 0)
            {
                return x / y;
            }
            else
            {
                throw new Exception("Division by zero");
            }
        }

    }

}
