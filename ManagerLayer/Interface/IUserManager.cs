using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel register);
        Task<RegisterModel> Login(LoginModel login);
        public string GetJWTToken(string userID);
        Task<bool> ForgotPassword(string emailID);
        Task<RegisterModel> ResetPassword(ResetModel resetPassword);
    }
}
