
using CommonLayer.Users;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    /// <summary>
    /// Add new user data code
    /// </summary>
    public class UserRl : IuserRl
    {
        FundooDbContext fundooDbContext;
        public IConfiguration Configuration { get; set; }
        public UserRl(FundooDbContext fundoo, IConfiguration configuration)
        {
            this.fundooDbContext = fundoo;
            this.Configuration = configuration;
        }
        public void AddUser(UserPostModel user)
        {
            try
            {
                User userdata = new User();
                userdata.UserId = user.userId;
                userdata.FirstName = user.firstName;
                userdata.LastName = user.lastName;
                userdata.Email = user.email;
                userdata.Password = user.password;
                fundooDbContext.Add(userdata);
                fundooDbContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }
        //Add login user code
        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundooDbContext.user.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (result == null)
                {
                    return null;
                }
                //call token for login user
                return GenerateJWTToken(email, result.UserId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //creating token for login
        private string GenerateJWTToken(string email, object userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userID",userId.ToString())
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

        //forgot password
        public bool ForgotPassword(string email)
        {
            try
            {
                var userdata = fundooDbContext.user.FirstOrDefault(u => u.Email == email);
                if (userdata == null)
                {
                    return false;
                }

                MessageQueue queue ;
                // Add Message to Queue

                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    queue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\FundooQueue");
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateJWTToken(email, userdata.UserId);
                MyMessage.Label = "Forgot Password Email";
                queue.Send(MyMessage);

                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(email, msg.Body.ToString());
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(MsmqQueue_ReciveCompleted);

                queue.BeginReceive();
                queue.Close();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void MsmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender;
                    Message msg = queue.EndReceive(e.AsyncResult);
                    EmailService.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                    queue.BeginReceive();
                }

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

        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email)
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

        //ResetPassword
        public bool ResetPassword(ResetPasswordModel resetPassword, string email)
        {
            try
            {
                if(resetPassword.NewPassword == resetPassword.ConfirmPassword)
                {
                    var result=fundooDbContext.user.Where(x => x.Email == email).FirstOrDefault();
                    result.Password=resetPassword.NewPassword;
                    fundooDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}        
