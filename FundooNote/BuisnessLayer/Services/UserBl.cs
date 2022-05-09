using BuisnessLayer.Interface;
using CommonLayer.Users;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UserBl : IuserBl
    {
        IuserRl userRl;

        public UserBl(IuserRl iuserRl)
        {
            this.userRl = iuserRl;
        }

        //Add user code
        public void AddUser(UserPostModel userPost)
        {
            try
            {
                this.userRl.AddUser(userPost);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Login new user
        public string LoginUser(string email, string password)
        {
            try
            {
                return this.userRl.LoginUser(email, password);  
            }
            catch
            {
                throw;
            }
        }

    }
}
