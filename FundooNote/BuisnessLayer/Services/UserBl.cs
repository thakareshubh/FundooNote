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
    }
}
