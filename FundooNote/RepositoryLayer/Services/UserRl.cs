
using CommonLayer.Users;
using Microsoft.Exchange.WebServices.Data;
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
        public UserRl(FundooDbContext fundoo,IConfiguration configuration)
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
            catch (Exception )
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
                if(result== null)
                {
                    return null;
                }
                return GenerateJWTToken(email, result.UserId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

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
    }
}
