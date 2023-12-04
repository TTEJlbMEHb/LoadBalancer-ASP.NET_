using LoadBalancer.BLL.Implementations;
using LoadBalancer.BLL.Interfaces;
using LoadBalancer.DAL.Implementations;
using LoadBalancer.DAL.Interfaces;
using LoadBalancer.DAL.Repositories;
using LoadBalancer.Domain.Entity;
using LoadBalancer.Models;

namespace LoadBalancer
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Calculator>, CalculatorRepository>();
            services.AddScoped<IBaseRepository<Matrix>, MatrixRepository>();            
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            services.AddScoped<IMatrixService, MatrixService>();
        }
    }
}
