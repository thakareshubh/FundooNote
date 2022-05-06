using CommonLayer.NewUser;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRl: IuserRl
    {
        FundooDbContext fundoosContext;
        public IConfiguration Configuration { get; }

        public void AddUser(User user)
        {
            try
            {
                User user1 = new User();
                user1.Id = user.Id;
                user1.FirstName = user.FirstName;
                user1.LastName = user.LastName; 
                user1.Email = user.Email;
                user1.Address = user.Address;
                fundoosContext.Add(user1);
                fundoosContext.SaveChanges();
            }
            catch (Exception )
            {

            }
        }
    }
}
