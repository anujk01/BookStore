using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                var result = await this.manager.Register(register);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "User Registered Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "User not Registered" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }

        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                var result = await this.manager.Login(login);
                if (result != null)
                {
                    string token = this.manager.GetJWTToken(login.emailID);
                    return this.Ok(new { Status = true, Message = "Login Successfully", Token = token });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Login Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string emailID)
        {
            try
            {
                bool result = await this.manager.ForgotPassword(emailID);
                if (result != false)
                {
                    return this.Ok(new { success = true, message = $"Mail Sent Successfully : {result}" });
                }
                return this.BadRequest(new { success = false, message = $"Failed to Sent Mail : {result}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        [HttpPut]
        [Route("resetPassword")]

        public async Task<IActionResult> ResetPassword(ResetModel resetPassword)
         {
            try
            {
                var result = await manager.ResetPassword(resetPassword);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<RegisterModel> { Status = true, Message = "Reset Password Successfully", Data = result});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reset Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
