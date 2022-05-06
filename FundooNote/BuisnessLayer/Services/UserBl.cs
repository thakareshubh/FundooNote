using BuisnessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UserBl : IuserBl
    {
        IuserRl iuserRl;
        public UserBl(IuserRl iuserRl)
        {
            this.iuserRl = iuserRl;
        }
        public void AddUser(User user)
        {
            try
            {
                this.iuserRl.AddUser(user);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
