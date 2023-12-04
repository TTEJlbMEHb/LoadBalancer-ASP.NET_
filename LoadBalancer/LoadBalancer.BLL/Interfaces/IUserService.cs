using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Response;
using LoadBalancer.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BLL.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Signup(SignupViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        //Task<BaseResponse<User>> GetProfile(long id);

        //Task<BaseResponse<User>> Save(User model);
    }
}
