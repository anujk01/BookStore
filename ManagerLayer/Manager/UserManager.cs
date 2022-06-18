using ManagerLayer.Interface;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository user;
        public UserManager(IUserRepository user)
        {
            this.user = user;

        }
        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                return await this.user.Register(register);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                return await this.user.Login(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetJWTToken(string emailID)
        {
            if (emailID == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("emailID", emailID)
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ForgotPassword(string emailID)
        {
            try
            {
                return await this.user.ForgotPassword(emailID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<RegisterModel> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                return await this.user.ResetPassword(resetPassword);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
