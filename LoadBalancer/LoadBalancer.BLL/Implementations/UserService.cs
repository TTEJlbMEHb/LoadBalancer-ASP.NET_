using LoadBalancer.BLL.Interfaces;
using LoadBalancer.DAL.Interfaces;
using LoadBalancer.Domain.Entity;
using LoadBalancer.Domain.Enum;
using LoadBalancer.Domain.Helpers;
using LoadBalancer.Domain.Response;
using LoadBalancer.Domain.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BLL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<ClaimsIdentity>> Signup(SignupViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "User is already exist",
                    };
                }
                user = new User()
                {
                    Email = model.Email,
                    Username = model.Username,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                    Role = Role.User,
                    HeavyTasks = new List<Matrix>()
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "User added",
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Signup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User does not exist",
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(model.Password)
                    || user.Email != model.Email)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Invalid email or password",
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Login] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        //public async Task<BaseResponse<User>> GetProfile(long id)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

        //        if (user == null)
        //        {
        //            return new BaseResponse<User>()
        //            {
        //                Description = "Not found",
        //                StatusCode = StatusCode.ObjectNotFound
        //            };
        //        }

        //        return new BaseResponse<User>()
        //        {
        //            Data = user,
        //            StatusCode = StatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<User>()
        //        {
        //            Description = $"[GetProfile] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}
    }
}
