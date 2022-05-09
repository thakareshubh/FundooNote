
using CommonLayer.Users;
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
    }
}
