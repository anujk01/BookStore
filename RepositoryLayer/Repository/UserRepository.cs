using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MSMQ.Messaging;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<RegisterModel> User;

        private readonly IConfiguration configuration;

        public UserRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            User = database.GetCollection<RegisterModel>("User");
        }

        public async Task<RegisterModel> Register(RegisterModel register)
        {
            try
            {
                var check = await this.User.AsQueryable().Where(x => x.emailID == register.emailID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.User.InsertOneAsync(register);
                    return register;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RegisterModel> Login(LoginModel login)
        {
            try
            {
                var result = await this.User.AsQueryable().Where(x => x.emailID == login.emailID).FirstOrDefaultAsync();
                if (result != null)
                {
                    result = await this.User.AsQueryable().Where(x => x.password == login.password).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ForgotPassword(string emailID)
        {
            try
            {
                var result = await this.User.AsQueryable().Where(u => u.emailID == emailID).FirstOrDefaultAsync();
                if (result == null)
                {
                    return false;
                }
                MessageQueue Book;
                if (MessageQueue.Exists(@".\Private$\BookStore"))
                {
                    Book = new MessageQueue(@".\Private$\BookStore");
                }
                else
                {
                    Book = MessageQueue.Create(@".\Private$\BookStore");
                }


                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GetJWTToken(emailID);
                message.Label = "Forgot Password BookStore";
                Book.Send(message);
                Message msg = Book.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(emailID, msg.Body.ToString());
                Book.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                Book.BeginReceive();
                Book.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message message = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GetJWTToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
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


        public async Task<RegisterModel> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                var result = await this.User.AsQueryable().Where(x => x.emailID == resetPassword.emailID).FirstOrDefaultAsync();
                if (result != null)
                {
                    await this.User.UpdateOneAsync(x => x.emailID == resetPassword.emailID,
                         Builders<RegisterModel>.Update.Set(x => x.password, resetPassword.Password));
                    return result;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
